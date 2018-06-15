using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DotNetGigs.Auth;
using DotNetGigs.Data;
using DotNetGigs.Helpers;
using DotNetGigs.Models;
using DotNetGigs.Models.Entities;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace DotNetGigs
{
    public class Startup
    {
        private const string SecretKey = "{D148FB1B-3FCF-40AE-811A-321270C35394}";
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public IConfiguration Configuration { get; }
        public Startup(IHostingEnvironment env, IConfiguration config)
        {
            //1
            //    var builder = new ConfigurationBuilder()
            //         .SetBasePath(env.ContentRootPath)
            //         .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //         .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            //         .AddEnvironmentVariables();
            //     Configuration = builder.Build();

            //2
            Configuration = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //entity
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("DotNetGigs")));
            //identity
            services
            .AddMvc() //(options => { options.Filters.Add(new RequireHttpsAttribute());
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
            

            // add identity
            var builder = services.AddIdentityCore<AppUser>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            IdentityModelEventSource.ShowPII = true; //для показа времени в логах


            services.AddAutoMapper();

            //jwt

            //factory to DI
            services.AddSingleton<IJwtFactory, JwtFactory>();
            services.Configure<FacebookAuthSettings>(Configuration.GetSection(nameof(FacebookAuthSettings)));

            services.Configure<GoogleAuthSettings>(Configuration.GetSection(nameof(GoogleAuthSettings)));



            //====read from cfg to model====
            //get
            var jwtOpt = Configuration.GetSection(nameof(JwtIssuerOptions));
            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtOpt[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtOpt[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);

            });
            //=============================


            //===JWT params 
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtOpt[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtOpt[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };


            //===Add JWT auth
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtOpt[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });


            services.AddAuthorization(options =>
            {
                options.AddPolicy("ViewUser", policy => policy.RequireClaim(ClaimRepository.ClaimTypes.AccessClaim, ClaimRepository.AccessClaimValues.View));
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(ClaimRepository.ClaimTypes.AccessClaim, ClaimRepository.AccessClaimValues.ApiAccess));
            });
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.Run(async (context) =>
            // {
            //     await context.Response.WriteAsync("Hello World!");
            // });

            app.UseExceptionHandler(
            builder =>
            {
                builder.Run(
                  async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        //context.Response.AddApplicationError(error.Error.Message);
                        //await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                       // context.Response.AddApplicationError(error.Error.Message);
                        await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                    }
                });
            });
            app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseStaticFiles();


            //роутинг чтобы работали ссылки SPA
            //
            app.UseMvc(r =>
            {
                app.UseMvc(routes =>
              {
                  routes.MapRoute(
                      name: "default",
                      template: "{controller=Home}/{action=Index}/{id?}");
              });

                // here you can see we make sure it doesn't start with /api, if it does, it'll 404 within .NET if it can't be found
                app.MapWhen(x => !x.Request.Path.Value.StartsWith("/api"), builder =>
                {
                    builder.UseMvc(routes =>
                    {
                        routes.MapSpaFallbackRoute(
                            name: "spa-fallback",
                            defaults: new { controller = "Home", action = "Index" });
                    });
                });
            });

            


        }
    }
}

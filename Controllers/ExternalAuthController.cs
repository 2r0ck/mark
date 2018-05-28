using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using DotNetGigs.Auth;
using DotNetGigs.Data;
using DotNetGigs.Helpers;
using DotNetGigs.Models;
using DotNetGigs.Models.Entities;
using DotNetGigs.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DotNetGigs.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ExternalAuthController : Controller
    {

        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly FacebookAuthSettings _fbAuthSettings;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private static readonly HttpClient Client = new HttpClient();

        public ExternalAuthController(IOptions<FacebookAuthSettings> fbAuthSettingsAccessor, UserManager<AppUser> userManager, Data.AppDbContext appDbContext, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _fbAuthSettings = fbAuthSettingsAccessor.Value;
            _userManager = userManager;
            _appDbContext = appDbContext;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Facebook([FromBody]FacebookAuthViewModel model)
        {
            var values = new Dictionary<string, string>();
            values.Add("code", model.AccessToken);
            values.Add("client_id", "313078532206-b01t045uahrop8264g97jrnt1j2dmbrt.apps.googleusercontent.com");
            values.Add("client_secret", "YZjprBnuzhD1K31y3F4reKXj");
            values.Add("redirect_uri", "http://localhost:5000/facebook-auth.html");
            values.Add("grant_type", "authorization_code");
            var appAccessTokenResponse = await Client.PostAsync("https://accounts.google.com/o/oauth2/token", new FormUrlEncodedContent(values));

            if (appAccessTokenResponse.StatusCode == HttpStatusCode.OK)
            {

                var res = await appAccessTokenResponse.Content.ReadAsStringAsync();
                var appAccessToken = JsonConvert.DeserializeObject<TokenPreseneter>(res);

                var userInfoResp = await Client.GetStringAsync($"https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token={appAccessToken.AccessToken}");
                var userInfo = JsonConvert.DeserializeObject<GoogleUserInfo>(userInfoResp);

                var user = await _userManager.FindByEmailAsync(userInfo.Email);

                if (user == null)
                {
                    var appUser = new AppUser
                    {
                        FirstName = userInfo.GivenName,
                        LastName = userInfo.FamilyName,
                        Email = userInfo.Email,
                        UserName = userInfo.Email,
                        PictureUrl = userInfo.Picture
                    };

                    var result = await _userManager.CreateAsync(appUser, Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8));
                    if (!result.Succeeded) 
                        return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));
                    _userManager.AddDefaultClaims(appUser);
                }

                // generate the jwt for the local user...
                var localUser = await _userManager.FindByNameAsync(userInfo.Email);

                if (localUser == null)
                {
                    return BadRequest(Errors.AddErrorToModelState("login_failure", "Failed to create local user account.", ModelState));
                }

                var identity = await _userManager.GetClaimsIdentity(localUser);
                var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, localUser.UserName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
            return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid google token.", ModelState));
        }
    }
}
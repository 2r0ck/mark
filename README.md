# DotNetGigs

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 1.7.4.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `-prod` flag for a production build.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via [Protractor](http://www.protractortest.org/).

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI README](https://github.com/angular/angular-cli/blob/master/README.md).



## Data Base
# Init

 >dotnet ef migrations add initial
 Create by context(AppDbContext) from assembly (DotNetGigs) migration files (folder  Migration) 
из Startup.cs
 public void ConfigureServices(IServiceCollection services)
        {            
 services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("DotNetGigs")));
...


 >dotnet ef database update 
 Apply changes to DB

## Tutorial
 https://fullstackmark.com/post/13/jwt-authentication-with-aspnet-core-2-web-api-angular-5-net-core-identity-and-facebook-login

 
 Marker -> charster:  Create the additional components\


Policy-based authorization in ASP.NET Core: 
https://docs.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-2.0

Role-based authorization in ASP.NET Core:
https://docs.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-2.0

Claims-based authorization in ASP.NET Core
https://docs.microsoft.com/en-us/aspnet/core/security/authorization/claims?view=aspnetcore-2.0
 
ASP.NET Core + Let's Encrypt
https://github.com/natemcmaster/LetsEncrypt

Google Auth
http://ankitsharmablogs.com/authentication-using-google-asp-net-core-2-0/

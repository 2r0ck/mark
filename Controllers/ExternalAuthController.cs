using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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
            values.Add("code",model.AccessToken);
            values.Add("client_id","313078532206-b01t045uahrop8264g97jrnt1j2dmbrt.apps.googleusercontent.com");
            values.Add("client_secret","YZjprBnuzhD1K31y3F4reKXj");
            values.Add("redirect_uri","http://localhost:5000/facebook-auth.html");
            values.Add("grant_type","authorization_code");
            var appAccessTokenResponse = await Client.PostAsync("https://accounts.google.com/o/oauth2/token",  new FormUrlEncodedContent(values));
            
            if(appAccessTokenResponse.StatusCode == HttpStatusCode.OK){
                    
                    var res = await appAccessTokenResponse.Content.ReadAsStringAsync();
                    var appAccessToken = JsonConvert.DeserializeObject<FacebookAppAccessToken>(res);

            }

           //https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token=ya29.GlvGBZknEkqkc55IOBvM5n7LjYiOIyL0jDE8Vh_35cltZ6To7eDCvcYm1bEb-Dmpr1oCUe2iMNNeZ-d5cD2Fa6iSS6w97tRvuSXdRZ2FpU8uHuzYbEEuzQzEgkNk

            return null;
            // 1.generate an app access token
          /*  var appAccessTokenResponse = await Client.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_fbAuthSettings.AppId}&client_secret={_fbAuthSettings.AppSecret}&grant_type=client_credentials");
            var appAccessToken = JsonConvert.DeserializeObject<FacebookAppAccessToken>(appAccessTokenResponse);
            
            // 2. validate the user access token
            var userAccessTokenValidationResponse = await Client.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={model.AccessToken}&access_token={appAccessToken.AccessToken}");
            var userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);
*/

/*
            if (!userAccessTokenValidation.Data.IsValid)
            {
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid facebook token.", ModelState));
            }

            // 3. we've got a valid token so we can request user data from fb
            var userInfoResponse = await Client.GetStringAsync($"https://graph.facebook.com/v2.8/me?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture&access_token={model.AccessToken}");
            var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);

            // 4. ready to create the local user account (if necessary) and jwt
            var user = await _userManager.FindByEmailAsync(userInfo.Email);

            if (user == null)
            {
                var appUser = new AppUser
                {
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    FacebookId = userInfo.Id,
                    Email = userInfo.Email,
                    UserName = userInfo.Email,
                    PictureUrl = userInfo.Picture.Data.Url
                };

                var result = await _userManager.CreateAsync(appUser, Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8));

                if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

                await _appDbContext.JobSeekers.AddAsync(new JobSeeker { IdentityId = appUser.Id, Location = "", Locale = userInfo.Locale, Gender = userInfo.Gender });
                await _appDbContext.SaveChangesAsync();
            }

            // generate the jwt for the local user...
            var localUser = await _userManager.FindByNameAsync(userInfo.Email);

            if (localUser == null)
            {
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Failed to create local user account.", ModelState));
            }

            var jwt = await Tokens.GenerateJwt(_jwtFactory.GenerateClaimsIdentity(localUser.UserName, localUser.Id),
              _jwtFactory, localUser.UserName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });

            return new OkObjectResult(jwt);
            */
        }





    }

}

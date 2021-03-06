using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DotNetGigs.Auth;
using DotNetGigs.Models;
using Newtonsoft.Json;

namespace DotNetGigs.Helpers
{
    public class Tokens
    {
        public static async Task<string> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
        {
            var response = new AuthResponse()
            {
                id = identity.Claims.Single(c => c.Type == ClaimRepository.ClaimTypes.IdClaim).Value,
                auth_token = await jwtFactory.GenerateEncodedToken(userName, identity),
                expires_in = (int)jwtOptions.ValidFor.TotalSeconds
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
    }
}
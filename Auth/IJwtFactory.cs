using System.Security.Claims;
using System.Threading.Tasks;

namespace DotNetGigs.Auth
{
     public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(string userName,Claim[] claims);
    }
}
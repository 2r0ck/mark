using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using DotNetGigs.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace DotNetGigs.Helpers
{
    public static class UserManagerExtension    
    {

        public static async void AddDefaultClaims(this UserManager<AppUser> um, AppUser user)   
        {
            await um.AddClaimAsync(user, new Claim(ClaimRepository.ClaimTypes.IdClaim,user.Id));
            await um.AddClaimAsync(user, new Claim(ClaimRepository.ClaimTypes.AccessClaim, ClaimRepository.AccessClaimValues.View));
        }
    }
}
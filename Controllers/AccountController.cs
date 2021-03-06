using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DotNetGigs.Data;
using DotNetGigs.Helpers;
using DotNetGigs.Models.Entities;
using DotNetGigs.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DotNetGigs.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller    
    {

      private readonly AppDbContext _appDbContext;        
      private readonly UserManager<AppUser> _userManager;
      private readonly IMapper _mapper;

      public AccountController(UserManager<AppUser> userManager,IMapper mapper,AppDbContext appDbContext)
      {
           _userManager = userManager;
            _mapper=mapper;
            _appDbContext=appDbContext; 
      }



        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = _mapper.Map<AppUser>(model);
            var result = await _userManager.CreateAsync(userIdentity, model.Password);
            
            if (!result.Succeeded){
              return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));
            }
            _userManager.AddDefaultClaims(userIdentity);
            
           #region old
            //by default user only view           
            // if(model.Email.Contains("admin"))
            // {
            //      await _userManager.AddClaimAsync(userIdentity, new Claim(ClaimRepository.ClaimTypes.AccessClaim, ClaimRepository.AccessClaimValues.ApiAccess));
            // }else{
            //      await _userManager.AddClaimAsync(userIdentity, new Claim(ClaimRepository.ClaimTypes.AccessClaim, ClaimRepository.AccessClaimValues.View));
            // }
            // await _appDbContext.JobSeekers.AddAsync(new JobSeeker { IdentityId = userIdentity.Id, Location = model.Location });
            // await _appDbContext.SaveChangesAsync();
            #endregion
            
           return new OkResult();
        }
    }
}
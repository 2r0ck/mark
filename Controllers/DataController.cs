using System.Linq;
using System.Threading.Tasks;
using DotNetGigs.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetGigs.Controllers
{

    [Route("api/[controller]")]
    public class DataController : Controller
    {


        [HttpGet("userid")]
        [Authorize(Policy = "ApiUser")]
        public IActionResult getUserId()
        {

            var idClaim = User.Claims.FirstOrDefault(x => x.Type == Consts.ClaimIdRole);
            if (idClaim == null)
                return new BadRequestObjectResult("User settings is invalid!");
            return new OkObjectResult($"id: {idClaim.Value}");
        }


        [HttpGet("publicData")]
        public IActionResult getpublicData()
        {

            return new OkObjectResult($" publicData !");
        }


    }
}
using System.Linq;
using System.Threading.Tasks;
using DotNetGigs.Helpers;
using DotNetGigs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DotNetGigs.Controllers
{

    [Route("api/[controller]")]
    public class DataController : Controller
    {


        [HttpGet("userid_api")]
        [Authorize(Policy = "ApiUser")]
        public IActionResult getUserId()
        {
            var idClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimRepository.ClaimTypes.IdClaim);
            if (idClaim == null)
                return new BadRequestObjectResult("User settings is invalid!");
            return new OkObjectResult($"id: {idClaim.Value}");
        }

         [HttpGet("userid_view")]
        [Authorize(Policy = "ViewUser")]
        public IActionResult getUserIdView()
        {
            var idClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimRepository.ClaimTypes.IdClaim);
            if (idClaim == null)
                return new BadRequestObjectResult("User settings is invalid!");
            return new OkObjectResult($"id: {idClaim.Value}");
        }


        [HttpGet("publicData")]
        public IActionResult getpublicData()
        {

            return new OkObjectResult(JsonConvert.SerializeObject(new TestData(){
                Id=1,
                Value = "success!"
            }));
        }


    }
}
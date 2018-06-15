using System.Linq;
using System.Collections;
using System.Threading.Tasks;
using AutoMapper;
using DotNetGigs.Data;
using DotNetGigs.Helpers;
using DotNetGigs.Models;
using DotNetGigs.ViewModels.Mapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DotNetGigs.Controllers
{

    [Route("api/[controller]")]
    public class DataController : Controller
    {
 
        private AppDbContext con;
        private IMapper mapper;
        public DataController(AppDbContext _con,IMapper _mapper)
        {
            con = _con;
            mapper = _mapper;
        }


        [HttpGet("userid_api")]
        [Authorize(Policy = "ApiUser")]
        public IActionResult getUserId()
        {
            var idClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimRepository.ClaimTypes.IdClaim);
            if (idClaim == null)
                return new BadRequestObjectResult("User settings is invalid!");
             return new OkObjectResult(JsonConvert.SerializeObject(new TestData(){
                Id=1,
                Value = "api"+idClaim.Value
            }));
        }

         [HttpGet("userid_view")]
        [Authorize(Policy = "ViewUser")]
        public IActionResult getUserIdView()
        {
            var idClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimRepository.ClaimTypes.IdClaim);
            if (idClaim == null)
                return new BadRequestObjectResult("User settings is invalid!");
            return new OkObjectResult(JsonConvert.SerializeObject(new TestData(){
                Id=1,
                Value = "view"+idClaim.Value
            }));
        }


        [HttpGet("publicData")]
        public IActionResult getpublicData()
        {

            return new OkObjectResult(JsonConvert.SerializeObject(new TestData(){
                Id=1,
                Value = "success!"
            }));
        }


        [HttpGet("getDataList")]
        [Authorize(Policy = "ViewUser")]
        public IActionResult getDataList()
        {
            var orders  = con.Orders.ToList();

            var ovm = orders.Select(x=>mapper.Map<OrderViewModel>(x)).ToList();

             return new JsonResult(new ValResponse<List<OrderViewModel>>(ovm));
        }


    }
}
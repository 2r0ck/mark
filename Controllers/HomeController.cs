using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetGigs.Controllers
{
    public class HomeController :  Controller 
    {
       // [Authorize(Policy = "ViewUser")]
        public ActionResult Index(string path)
        {
            return File("~/index.html", "text/html");
        }

    }
}
using Microsoft.AspNetCore.Mvc;

namespace DotNetGigs.Controllers
{
    public class HomeController :  Controller 
    {
        public ActionResult Index(string path)
        {
            return File("~/index.html", "text/html");
        }

    }
}
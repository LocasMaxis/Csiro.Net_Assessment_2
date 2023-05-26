using Microsoft.AspNetCore.Mvc;

namespace Csiro.Controllers
{
    public class HomeController : Controller
    {
      
        public IActionResult Index()
        {
           
            return View();

        }

    }
}

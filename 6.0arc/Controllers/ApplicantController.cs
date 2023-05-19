using Microsoft.AspNetCore.Mvc;

namespace Csiro.Controllers
{
    public class ApplicantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

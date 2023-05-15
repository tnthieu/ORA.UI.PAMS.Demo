using Microsoft.AspNetCore.Mvc;

namespace ORA_UI_PAMS_Demo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Grid()
        {
            return PartialView("~/Views/Home/_SpecialNoteLargeGRID.cshtml");
        }
    }
}

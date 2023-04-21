using Microsoft.AspNetCore.Mvc;

namespace FlightSearchApp.Presentation
{
    public class HomeController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }
    }
}

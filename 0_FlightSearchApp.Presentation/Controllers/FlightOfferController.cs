using Microsoft.AspNetCore.Mvc;

namespace FlightSearchApp.Presentation
{
    public class FlightOfferController : Controller
    {
        public IActionResult FlightOfferIndex()
        {
            return View();
        }
    }
}

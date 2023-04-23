using FlightSearchApp.Application;
using FlightSearchApp.Domain;
using Microsoft.AspNetCore.Mvc;

namespace FlightSearchApp.Presentation
{
    public class FlightOfferController : Controller
    {
        public ICodeListService CodeListService;
        public FlightOfferController(ICodeListService codeListService)
        {
            CodeListService = codeListService;
        }
        public IActionResult FlightOfferIndex()
        {
            return View();
        }

        public JsonResult FlightOfferGetCurrencyForCombo()
        {
            return Json(CodeListService.ReadAllForEntity(CodesEnum.Values));
        }
    }
}

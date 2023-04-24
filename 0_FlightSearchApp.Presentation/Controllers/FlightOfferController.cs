using AmadeusService;
using FlightSearchApp.Application;
using FlightSearchApp.Domain;
using Microsoft.AspNetCore.Mvc;

namespace FlightSearchApp.Presentation
{
    public class FlightOfferController : Controller
    {
        public ICodeListService CodeListService;
        public IAmadeusDataService AmadeusDataService;
        public IFlightOfferService FlightOfferService;
        public FlightOfferController(ICodeListService codeListService, IAmadeusDataService amadeusDataService, IFlightOfferService flightOfferService)
        {
            CodeListService = codeListService;
            AmadeusDataService = amadeusDataService;
            FlightOfferService = flightOfferService;
        }
        public IActionResult FlightOfferIndex()
        {
            return View();
        }

        public JsonResult FlightOfferGetCurrencyForCombo()
        {
            return Json(CodeListService.ReadAllForEntityForCombo(CodesEnum.Values));
        }

        public void FlightOfferSearch(string originLocationCode, string destinationLocationCode, DateTime departureDate, DateTime? returnDate, int adults, string? currencyCode)
        {
            var currencies = CodeListService.ReadAllForEntity(CodesEnum.Values);
            var flightOffers = AmadeusDataService.FlightOfferSearch(originLocationCode, destinationLocationCode, departureDate, returnDate, adults, currencyCode, currencies);
            FlightOfferService.CheckAndSaveData(flightOffers);
        }
    }
}

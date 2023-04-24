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

        public string FlightOfferSynchronize(string originLocationCode, string destinationLocationCode, string departureDate, string? returnDate, string adults, string? currencyCode)
        {
            var currencies = CodeListService.ReadAllForEntity(CodesEnum.Values);
            var flightOffers = AmadeusDataService.FlightOfferSynchronize(originLocationCode, destinationLocationCode, departureDate, returnDate, adults, currencyCode, currencies);
            return flightOffers.Result == null ? "There are no flight offers for the entered values" : FlightOfferService.CheckAndSaveData(flightOffers);
        }
        public JsonResult FlightOfferSearch(string originLocationCode, string destinationLocationCode, string departureDate, string? returnDate, string adults, string? currencyCode)
        {
            return Json(new
            {
                aaData = FlightOfferService.ReadForDT(originLocationCode, destinationLocationCode, departureDate, returnDate, adults, currencyCode)
            });
        }
    }
}

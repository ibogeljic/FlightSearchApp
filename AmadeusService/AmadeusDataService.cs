using amadeus;
using FlightSearchApp.Domain;
using FlightSearchApp.Application;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace AmadeusService
{
    #region Interfaces
    public interface IAmadeusDataService
    {
        Task<List<FlightOffer>> FlightOfferSearch(string originLocationCode, string destinationLocationCode, DateTime departureDate, DateTime? returnDate, int adults, 
            string? currencyCode, List<CodeList> values);
    }
    #endregion

    public class AmadeusDataService : IAmadeusDataService
    { 
        #region Services

        public async Task<List<FlightOffer>> FlightOfferSearch(string originLocationCode, string destinationLocationCode, DateTime departureDate, DateTime? returnDate, int adults, 
            string? currencyCode, List<CodeList> values)
        {
            try
            {
                var amadeus = new Amadeus(new Configuration(Globals.ApiKey, Globals.ApiSecret));
                var requestParams = Params.with("originLocationCode", "ZAG")
                        .and("destinationLocationCode", "BEG")
                        .and("departureDate", "2023-04-25")
                        .and("adults", "1");
                if (returnDate != null)
                    requestParams = requestParams.and("returnDate", "2023-04-26");
                if (currencyCode != null)
                    requestParams = requestParams.and("currencyCode", "HRK");

                var response = await Task.Run(() => amadeus.get("/v2/shopping/flight-offers", requestParams));

                var flightOffers = new List<FlightOffer>();
                foreach (JToken flightOfferToken in response.data)
                {
                    JObject offer = (JObject)flightOfferToken;
                    JObject price = (JObject)offer["price"];
                    JObject outbound = (JObject)offer["itineraries"][0];
                    JObject? inbound = returnDate == null ? null : (JObject)offer["itineraries"][1];

                    var flightOffer = new FlightOffer()
                    {
                        DepartureAirportCode = (string)outbound["segments"][0]["departure"]["iataCode"],
                        DestinationAirportCode = (string)outbound["segments"][outbound["segments"].Count() - 1]["arrival"]["iataCode"],
                        DepartureDate = DateTime.ParseExact((string)outbound["segments"][0]["departure"]["at"], "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                        ReturnDate = inbound == null ? null : DateTime.ParseExact((string)inbound["segments"][0]["departure"]["at"], "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                        TransferNumbersDeparture = outbound["segments"].Count() - 1,
                        TransferNumbersReturn = inbound == null ? 0 : inbound["segments"].Count() - 1,
                        PassengersNumber = adults,
                        Value = values.Where(v => v.Code == (string)price["currency"]).First(),
                        ValueID = values.Where(v => v.Code == (string)price["currency"]).First().ID,
                        TotalPrice = Convert.ToDouble(price["total"])
                    };

                    flightOffers.Add(flightOffer);
                }

                return flightOffers;
            }
            catch (Exception e) { return null; }
        }

        #endregion
    }
}

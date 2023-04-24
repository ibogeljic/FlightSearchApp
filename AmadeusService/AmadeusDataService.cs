using amadeus;
using FlightSearchApp.Domain;
using FlightSearchApp.Application;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;

namespace AmadeusService
{
    #region Interfaces
    public interface IAmadeusDataService
    {
        Task<List<FlightOffer>> FlightOfferSynchronize(string originLocationCode, string destinationLocationCode, string departureDate, string? returnDate, string adults, 
            string? currencyCode, List<CodeList> values);
    }
    #endregion

    public class AmadeusDataService : IAmadeusDataService
    { 
        #region Services

        public async Task<List<FlightOffer>> FlightOfferSynchronize(string originLocationCode, string destinationLocationCode, string departureDate, string? returnDate, string adults, 
            string? currencyCode, List<CodeList> values)
        {
            string departureDateFormat = DateTime.ParseExact(departureDate, "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
            string? returnDateFormat = string.IsNullOrEmpty(returnDate) ? null : DateTime.ParseExact(returnDate, "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

            try
            {
                var amadeus = new Amadeus(new Configuration(Globals.ApiKey, Globals.ApiSecret));
                var requestParams = Params.with("originLocationCode", originLocationCode)
                        .and("destinationLocationCode", destinationLocationCode)
                        .and("departureDate", departureDateFormat)
                        .and("adults", adults);
                if (returnDate != null)
                    requestParams = requestParams.and("returnDate", returnDateFormat);
                if (currencyCode != null)
                    requestParams = requestParams.and("currencyCode", currencyCode);

                var response = await Task.Run(() => amadeus.get("/v2/shopping/flight-offers", requestParams));

                var flightOffers = new List<FlightOffer>();
                var noValues = values == null || values.Count == 0;
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
                        PassengersNumber = Convert.ToInt32(adults),
                        Value = noValues ? null : values.First(v => v.Code == (string)price["currency"]),
                        ValueID = noValues ? null : values.First(v => v.Code == (string)price["currency"]).ID,
                        TotalPrice = Convert.ToDouble(price["total"])
                    };

                    flightOffers.Add(flightOffer);
                }

                return flightOffers;
            }
            catch (Exception e) 
            { 
                return null; 
            }
        }

        #endregion
    }
}

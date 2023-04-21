using amadeus;

namespace AmadeusService
{
    internal class AmadeusService
    {
        public void GetData(string originLocationCode, string destinationLocationCode, DateTime departureDate, DateTime? returnDate, int adults, string? currencyCode)
        {
            //opcionalni parametri su: returnDate i currencyCode
            var amadeus = new Amadeus(new Configuration("RAeeWhyKxcjdEC5PW5lXD82GKg3OKvsY", "voPLTbIAjkIhBkjC"));

            var testFlightOffers = amadeus.get("/v2/shopping/flight-offers",
                Params.with("originLocationCode", "ZAG")
                    .and("destinationLocationCode", "IST")
                    .and("departureDate", "2023-04-21")
                    //.and("returnDate", "2023-05-16")
                    .and("adults", "2")
                    .and("currencyCode", "HRK")
                    //.and("fields", "id,price.total")
                    //.and("exclude", "itineraries,validatingAirlineCodes")
                    );

            var data = testFlightOffers.dataString;
        }
    }
}

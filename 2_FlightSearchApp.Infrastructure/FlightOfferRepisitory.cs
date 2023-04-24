using FlightSearchApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSearchApp.Infrastructure
{
    public class FlightOfferRepisitory : GenericRepository<FlightOffer>, IFlightOfferRepository
    {
        public FlightOfferRepisitory() : base()
        {
        }
        
        public void CheckAndSaveNonDuplicate(FlightOffer flightOffer)
        {
            if (!QueryDbset().Where(f => f.DepartureAirportCode == flightOffer.DepartureAirportCode && f.DestinationAirportCode == flightOffer.DestinationAirportCode && 
                f.DepartureDate == flightOffer.DepartureDate && f.ReturnDate == flightOffer.ReturnDate && f.PassengersNumber == flightOffer.PassengersNumber && 
                f.ValueID == flightOffer.ValueID).Any())
                CreateSave(flightOffer);
        }
        public IEnumerable<FlightOffer> ReadForParameters(string originLocationCode, string destinationLocationCode, DateTime departureDate, DateTime? returnDate, int adults, 
            string? currencyCode)
        {
            var test = QueryDbset()
                .Where(f => f.DepartureAirportCode == originLocationCode && f.DestinationAirportCode == destinationLocationCode && f.DepartureDate.Year == departureDate.Year &&
                    f.DepartureDate.Month == departureDate.Month && f.DepartureDate.Day == departureDate.Day && (returnDate == null ? f.ReturnDate == null : f.ReturnDate != null &&
                    ((DateTime)f.ReturnDate).Year == ((DateTime)returnDate).Year && ((DateTime)f.ReturnDate).Month == ((DateTime)returnDate).Month && ((DateTime)f.ReturnDate).Day ==
                    ((DateTime)returnDate).Day) && f.PassengersNumber == adults && (currencyCode != null ? f.ValueID != null && f.Value.Code == currencyCode : f.ValueID == null))
                .AsEnumerable();
            return QueryDbset()
                .Where(f => f.DepartureAirportCode == originLocationCode && f.DestinationAirportCode == destinationLocationCode && f.DepartureDate.Year == departureDate.Year && 
                    f.DepartureDate.Month == departureDate.Month && f.DepartureDate.Day == departureDate.Day && (returnDate == null ? f.ReturnDate == null : f.ReturnDate != null &&
                    ((DateTime)f.ReturnDate).Year == ((DateTime)returnDate).Year && ((DateTime)f.ReturnDate).Month == ((DateTime)returnDate).Month && ((DateTime)f.ReturnDate).Day == 
                    ((DateTime)returnDate).Day) && f.PassengersNumber == adults && (currencyCode != null ? f.ValueID != null && f.Value.Code == currencyCode : f.ValueID == null))
                .AsEnumerable();
        }
    }
}

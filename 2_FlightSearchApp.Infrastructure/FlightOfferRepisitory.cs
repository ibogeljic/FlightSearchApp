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
        //public IEnumerable<FlightOffer> ReadAllActiveOrdered()
        //{
        //    return QueryDbsetActive()
        //        .OrderBy(s => s.Entity)
        //        .AsEnumerable();
        //}
        public void CheckAndSaveNonDuplicate(FlightOffer flightOffer)
        {
            if (!QueryDbset().Where(f => f.DepartureAirportCode == flightOffer.DepartureAirportCode && f.DestinationAirportCode == flightOffer.DestinationAirportCode && 
                f.DepartureDate == flightOffer.DepartureDate && f.ReturnDate == flightOffer.ReturnDate && f.PassengersNumber == flightOffer.PassengersNumber && 
                f.ValueID == flightOffer.ValueID).Any())
                CreateSave(flightOffer);
        }
    }
}

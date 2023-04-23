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
    }
}

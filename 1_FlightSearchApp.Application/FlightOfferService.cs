﻿using FlightSearchApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSearchApp.Application
{
    #region Interfaces
    public interface IFlightOfferService : IGenericService<FlightOffer>
    {
        //List<string[]> ReadForDT(string entity);
        void CheckAndSaveData(Task<List<FlightOffer>> flightOffers);
    }
    #endregion

    #region Services
    public class FlightOfferService : GenericService<FlightOffer>, IFlightOfferService
    {
        private readonly IFlightOfferRepository _Repository;
        public FlightOfferService(IFlightOfferRepository repository) : base(repository)
        {
            _Repository = repository;
        }
        public void CheckAndSaveData(Task<List<FlightOffer>> flightOffers)
        {
            foreach (var flightOffer in flightOffers.Result)
                _Repository.CheckAndSaveNonDuplicate(flightOffer);
        }
    }
    #endregion
}

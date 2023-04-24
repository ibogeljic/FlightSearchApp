using FlightSearchApp.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FlightSearchApp.Application
{
    #region Interfaces
    public interface IFlightOfferService : IGenericService<FlightOffer>
    {
        string CheckAndSaveData(Task<List<FlightOffer>> flightOffers);
        List<string[]> ReadForDT(string originLocationCode, string destinationLocationCode, string departureDate, string? returnDate, string adults, string? currencyCode);
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
        public string CheckAndSaveData(Task<List<FlightOffer>> flightOffers)
        {
            try
            {
                foreach (var flightOffer in flightOffers.Result)
                    _Repository.CheckAndSaveNonDuplicate(flightOffer);

                return "Data synchronization completed!";
            }
            catch (Exception ex) 
            {
                return ex.Message;
            }
        }
        public List<string[]> ReadForDT(string originLocationCode, string destinationLocationCode, string departureDate, string? returnDate, string adults, string? currencyCode)
        {
            var rows = new List<string[]> { };
            DateTime departureDateFormat = DateTime.ParseExact(departureDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            DateTime? returnDateFormat = string.IsNullOrEmpty(returnDate) ? null : DateTime.ParseExact(returnDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);

            var list = _Repository.ReadForParameters(originLocationCode, destinationLocationCode, departureDateFormat, returnDateFormat, Convert.ToInt32(adults), currencyCode).ToList();
            
            list.ForEach(s =>
            {
                rows.Add(
                    new string[]
                    {
                        s.DepartureAirportCode,
                        s.DestinationAirportCode,
                        Globals.DateTimeToString(s.DepartureDate),
                        Globals.DateTimeToString(s.ReturnDate),
                        Globals.IntToString(s.TransferNumbersDeparture),
                        Globals.IntToString(s.TransferNumbersReturn),
                        Globals.IntToString(s.PassengersNumber),
                        s.Value == null ? "" : s.Value.Code,
                        Globals.DoubleToString(s.TotalPrice)
                    });
            });

            return rows;
        }
    }
    #endregion
}

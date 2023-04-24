using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSearchApp.Domain
{
    public class FlightOffer : Entity
    {
        public string DepartureAirportCode { get; set; }
        public string DestinationAirportCode { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int TransferNumbersDeparture { get; set; }
        public int TransferNumbersReturn { get; set; }
        public int PassengersNumber { get; set; }
        public int? ValueID { get; set; }
        [ForeignKey("ValueID")]
        public CodeList Value { get; set; }
        public double TotalPrice { get; set; }

        //public FlightOffer() { }
        //public FlightOffer(string departureAirportCode, string destinationAirportCode, DateTime departureDate, DateTime? returnDate, int transferNumbersDeparture,
        //    int transferNumbersReturn, int passengersNumber, int? valueID, CodeList value, double totalPrice)
        //{
        //    DepartureAirportCode = departureAirportCode;
        //    DestinationAirportCode = destinationAirportCode;    
        //    DepartureDate = departureDate;
        //    ReturnDate = returnDate;
        //    TransferNumbersDeparture = transferNumbersDeparture;
        //    TransferNumbersReturn = transferNumbersReturn;
        //    PassengersNumber = passengersNumber;
        //    ValueID = valueID;
        //    Value = value;
        //    TotalPrice = totalPrice;
        //}
    }
}

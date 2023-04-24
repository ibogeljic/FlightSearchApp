using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSearchApp.Domain
{
    public enum CodesEnum
    {
        Values = 1,
        AirportCodes = 2
    }

    public enum AjaxPath
    {
        CodeListGetEntitiesForCombo = 1,
        CodeListGetEntitiesForFilterCombo = 2,
        CodeListReadForDT = 3,
        CodeListAdd = 4,
        CodeListUpdate = 5,
        CodeListDelete = 6,
        FlightOfferGetCurrencyForCombo = 7,
        FlightOfferSynchronize = 8,
        FlightOfferSearch = 9
    }
}

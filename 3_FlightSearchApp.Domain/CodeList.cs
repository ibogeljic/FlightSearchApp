using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSearchApp.Domain
{
    public class CodeList : EntitySoftDelete
    {
        public string Entity { get; set; }
        public string Code { get; set; }

        public string UpdateTD { get { return Globals.UpdateTD("btnCodeListUpdate", new string[] { Globals.IntToString(ID), Code, Entity }); } }
        public string DeleteTD { get { return Globals.DeleteTD(Globals.GetAjaxPath(AjaxPath.CodeListDelete), ID, "codeListDelete"); } }
    }
}

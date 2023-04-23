
namespace FlightSearchApp.Domain
{
    public class Globals
    {
        //Želio bi napomenuti da u svakoj drugoj situaciji, ovi podaci ne bi bili vidljivi na ovaj način, nego kriptirani u bazi
        private const string _ApiKey = "5geRD4x0etjNcGZQUiOR0laAiEzALhyl";  
        private const string _ApiSecret = "9bIT6qmJtE4Pp1ug";

        public string ApiKey => _ApiKey;
        public string ApiSecret => _ApiSecret;
        public const char DefaultStringDelimiter = '|';
        public const string JquerySeparator = "\xA0\xA0\xA0\xA0";

        public static string GetAjaxPath(AjaxPath path)
        {
            var pathString = "";

            switch (path)
            {
                case AjaxPath.CodeListGetEntitiesForCombo:
                    pathString += "/CodeList/CodeListGetEntitiesForCombo";
                    break;
                case AjaxPath.CodeListGetEntitiesForFilterCombo:
                    pathString += "/CodeList/CodeListGetEntitiesForFilterCombo";
                    break;
                case AjaxPath.CodeListReadForDT:
                    pathString += "/CodeList/CodeListReadForDT";
                    break;
                case AjaxPath.CodeListAdd:
                    pathString += "/CodeList/CodeListAdd";
                    break;
                case AjaxPath.CodeListUpdate:
                    pathString += "/CodeList/CodeListUpdate";
                    break;
                case AjaxPath.FlightOfferGetCurrencyForCombo:
                    pathString += "/FlightOffer/FlightOfferGetCurrencyForCombo";
                    break;
            }
            return pathString;
        }

        public static string UpdateTD(string btnName, string[] data)
        {
            return string.Format("<a id='{0}' data='{1}' class='btn btn-xs btn-default list-options'><i class='fas fa-pencil-alt'></i></a>", btnName, string.Join(DefaultStringDelimiter, data));
        }
        public static string DeleteTD(string url, int id, string method)
        {
            return string.Format("<a href='{0}/?id={1}' data='{2}' class='btn btn-xs btn-default list-options btn-delete'><i class='fa fa-trash'></i></a>", url, id, method);
        }
        public static string IntToString(int number)
        {
            return number.ToString();
        }
        public static object ReadForCombo(bool mandatory, IEnumerable<object> list)
        {
            var objectList = list.ToList();

            if (!mandatory)
                objectList.Insert(0, ComboEmptyRow());

            return objectList.ToArray();
        }
        public static object ComboEmptyRow()
        {
            return new { id = "", text = JquerySeparator };
        }
    }
}

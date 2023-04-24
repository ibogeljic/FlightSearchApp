
namespace FlightSearchApp.Domain
{
    public class Globals
    {
        //Želio bi napomenuti da u svakoj drugoj situaciji, ovi podaci ne bi bili vidljivi na ovaj način, nego kriptirani u bazi
        public const string ApiKey = "5geRD4x0etjNcGZQUiOR0laAiEzALhyl";
        public const string ApiSecret = "9bIT6qmJtE4Pp1ug";

        public const char DefaultStringDelimiter = '|';
        public const string JquerySeparator = "\xA0\xA0\xA0\xA0";
        public const string DefaultDate = "dd.MM.yyyy";

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
                case AjaxPath.FlightOfferSearch:
                    pathString += "/FlightOffer/FlightOfferSearch";
                    break;
                case AjaxPath.FlightOfferSynchronize:
                    pathString += "/FlightOffer/FlightOfferSynchronize";
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
        public static string IntToString(int number)
        {
            return number.ToString();
        }
        public static string IntToString(int? number)
        {
            return number == null ? "" : IntToString(number.Value);
        }
        public static string DoubleToString(double number)
        {
            return number == 0 ? "0,00" : number.ToString("N2");
        }
        public static string DoubleToString(double? broj)
        {
            return broj == null ? "" : DoubleToString((double)broj);
        }
        public static string DateTimeToString(DateTime date)
        {
            return date.ToString(DefaultDate);
        }
        public static string DateTimeToString(DateTime? date)
        {
            return date == null ? "" : DateTimeToString((DateTime)date);
        }
    }
}

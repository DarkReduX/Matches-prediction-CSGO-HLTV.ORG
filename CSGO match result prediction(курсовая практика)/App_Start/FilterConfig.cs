using System.Web;
using System.Web.Mvc;

namespace CSGO_match_result_prediction_курсовая_практика_
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

using EDISAngular.APIControllers;
using System.Web;
using System.Web.Mvc;

namespace EDISAngular
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

        }
    }
}

using System.Web;
using System.Web.Mvc;

namespace Metropole
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // With custom errors on this displays the error.cshtml view on unhandled 500+ errors.
            // Does not affect 404 errors.
            filters.Add(new HandleErrorAttribute());
        }
    }
}

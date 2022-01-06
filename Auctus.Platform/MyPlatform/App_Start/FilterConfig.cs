using MyPlatform.Filter;
using System.Web;
using System.Web.Mvc;

namespace MyPlatform
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new WebAuthorization());
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new MyExceptionAttribute());

        }
    }
}

using System.Web.Mvc;

namespace WAVE.Website.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
#if !DEBUG
            filters.Add(new RequireHttpsAttribute());
#endif
            //filters.Add(new HandleErrorAttribute());
            //filters.Add(new AuthorizeAttribute());
        }
    }
}
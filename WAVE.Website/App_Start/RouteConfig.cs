using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WAVE.Website.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("MessagesCompose", "Messages/Compose", new {controller = "Messages", action = "Compose"});
            routes.MapRoute("MessagesDetails", "Messages/Details/{userName}",
                new {controller = "Messages", action = "Details"});
            routes.MapRoute("MessagesIndex", "Messages/Index", new {controller = "Messages", action = "Index"});
            routes.MapRoute("MessagesUser", "Messages/{userName}", new {controller = "User", action = "Details"});
                // new { userName = @"([-.]*\w+[-.]*\w+)*" });


            routes.MapRoute("UserOtherActions", "User/{action}/{userName}",
                new {controller = "User", userName = UrlParameter.Optional},
                new
                {
                    action =
                        new FromValuesListConstraint("Follow", "Edit", "Unfollow", "UploadFileProfile",
                            "UploadFileBanner")
                }
                );

            routes.MapRoute("UserIndex", "User/Index", new {controller = "User", action = "Default"});

            routes.MapRoute("User", "User/{userName}",
                new {controller = "User", action = "Index", userName = UrlParameter.Optional});
                // new { userName = @"([-.]*\w+[-.]*\w+)*" });

            routes.MapRoute("UserDefault", "User", new {controller = "User", action = "Default"});

            routes.MapRoute("Contact", "Contact", new {controller = "Contact", action = "Index"});
            routes.MapRoute("OurSupporters", "About/Our-Supporters",
                new {controller = "About", action = "OurSupporters"});

            routes.MapRoute("TheTeam", "The-Team", new {controller = "About", action = "TheTeam"});
            routes.MapRoute("TermsOfUse", "Terms-Of-Use", new {controller = "About", action = "TermsOfUse"});
            routes.MapRoute("PrivacyPolicy", "Privacy-Policy", new {controller = "About", action = "PrivacyPolicy"});
            routes.MapRoute("FAQS", "FAQs", new {controller = "About", action = "FAQs"});
            routes.MapRoute("About", "About", new {controller = "About", action = "Index"});
            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional}
                );
        }
    }


    public class FromValuesListConstraint : IRouteConstraint
    {
        private readonly string[] _values;

        public FromValuesListConstraint(params string[] values)
        {
            _values = values;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName,
            RouteValueDictionary values, RouteDirection routeDirection)
        {
            // Get the value called "parameterName" from the
            // RouteValueDictionary called "value"

            var value = values[parameterName].ToString();

            // Return true is the list of allowed values contains
            // this value.

            for (var i = 0; i < _values.Length; i++)
                if (SContains(_values[i], value, StringComparison.OrdinalIgnoreCase))
                    return true;

            return false;
        }

        public bool SContains(string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }
    }
}
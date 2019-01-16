using Microsoft.Web.WebPages.OAuth;

namespace WAVE.Website.App_Start
{
    public class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            OAuthWebSecurity.RegisterFacebookClient("575771852499694", "404efc217504625e7f789833f8a1ad14");

            OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
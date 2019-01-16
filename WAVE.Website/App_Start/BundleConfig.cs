using System.Web.Optimization;

namespace WAVE.Website.App_Start
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterBundleScripts(bundles);
            RegisterBundleStyles(bundles);
        }

        private static void RegisterBundleScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/JQuery/jquery-2.0.3.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/JQuery/jquery.unobtrusive*",
                "~/Scripts/JQuery/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/Modernizr/modernizr-2.6.2.js"));

            bundles.Add(new ScriptBundle("~/bundles/BootstrapJs").Include(
                "~/Scripts/JQuery/jquery.unobtrusive*",
                "~/Scripts/JQuery/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/IEJsHelpers").Include(
                "~/Scripts/Helpers/html5shiv.js",
                "~/Scripts/Helpers/mq.js",
                "~/Scripts/Helpers/ie8_fix_maxwidth.js"));

            bundles.Add(new ScriptBundle("~/bundles/Custom").Include(
                "~/Scripts/Custom/custom.js"));
        }

        private static void RegisterBundleStyles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/css/site.css"));

            bundles.Add(new StyleBundle("~/bundles/Bootstrapcss").Include(
                "~/Content/css/bootstrap/bootstrap.css",
                "~/Content/css/bootstrap/bootstrap-progressbar-2.3.2.min.css",
                "~/Content/css/bootstrap/font-awesome-ie7.css",
                "~/Content/css/bootstrap/font-awesome.css"));

            bundles.Add(new StyleBundle("~/bundles/IECSSHelpers").Include(
                "~/Contentcss/helpers/customIE.css"));
        }
    }
}
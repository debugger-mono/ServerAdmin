using System.Web.Optimization;

namespace ServerAdmin
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/MetisMenu.css",
                      "~/Content/MorrisChart.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/Ie8Scripts").Include(
                      "~/Scripts/html5shiv.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/Libs").Include(
                    "~/Scripts/jquery-{version}.js",
                      "~/Scripts/underscore.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/CryptoJS v3.1.2/rollups/aes.js"));

            bundles.Add(new ScriptBundle("~/bundles/AngularJs").Include(
                      "~/Scripts/angular.js"));
        }
    }
}

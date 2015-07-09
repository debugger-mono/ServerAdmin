using System;
using System.Web.Optimization;

namespace Tbl.ServerAdmin.Web
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{

			bundles.Add(new StyleBundle("~/Content/css").Include(
				"~/Content/font-awesome-4.3.0/css/font-awesome.css",
				"~/Content/bootstrap.css",
				"~/Content/timeline.css",
				"~/Content/site.css"));

			bundles.Add(new StyleBundle("~/Content/pluginsCss").Include(
				"~/Scripts/plugins/metisMenu/metisMenu.css",
				"~/Scripts/plugins/morris/morrisChart.css",
				"~/Scripts/plugins/angular-loading-bar/loading-bar.css"
			));

			bundles.Add(new ScriptBundle("~/bundles/pluginsJs").Include(
				"~/Scripts/plugins/metisMenu/metisMenu.js",
				"~/Scripts/plugins/morris/morrisChart.js",
				"~/Scripts/plugins/angular-loading-bar/loading-bar.js"));

			bundles.Add(new ScriptBundle("~/bundles/Ie8Scripts").Include(
				"~/Scripts/html5shiv.js",
				"~/Scripts/respond.js"));

			bundles.Add(new ScriptBundle("~/bundles/Libs").Include(
				"~/Scripts/jquery-{version}.js",
				"~/Scripts/underscore.js",
				"~/Scripts/bootstrap.js",
				"~/Scripts/CryptoJS v3.1.2/rollups/aes.js"));

			bundles.Add(new ScriptBundle("~/bundles/AngularJs").Include(
				"~/Scripts/Angular-1.4.1/angular.js",
				"~/Scripts/Angular-1.4.1/angular-sanitize.js",
				"~/Scripts/Angular-1.4.1/angular-route.js",
				"~/Scripts/Angular-1.4.1/angular-resource.js",
				"~/Scripts/Angular-1.4.1/angular-loader.js",
				"~/Scripts/Angular-1.4.1/angular-cookies.js",
				"~/Scripts/ui-router/angular-ui-router.js"));

			bundles.Add(new ScriptBundle("~/bundles/AppJs").Include(
				"~/App/app.js",
				"~/App/app.services.js",
				"~/App/app.directives.js",
				"~/App/Controllers/app.controllers.*",
				"~/App/Directives/app.directives.*",
				"~/Scripts/sb-admin-2.js"));

			BundleTable.EnableOptimizations = false;
		}
	}
}


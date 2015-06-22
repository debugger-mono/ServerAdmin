using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace ServerAdmin.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Action based route, the action name must not start with HttpVerb name
            // Supported HttpVerb should also be specified as attribute on Action Name
            config.Routes.MapHttpRoute(
                name: "Api_Action",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional });

            // HttpVerb based route
            config.Routes.MapHttpRoute(
                name: "Api_NoAction",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
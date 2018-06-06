using System.Web.Http;
using System.Diagnostics.CodeAnalysis;


namespace MortgageCalculator.Api
{
    public static class WebApiConfig
    {
        [ExcludeFromCodeCoverage]
        public static void Register(HttpConfiguration config)
        {
           
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

using System.Web.Http;
using System.Diagnostics.CodeAnalysis;

namespace MortgageCalculator.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        [ExcludeFromCodeCoverage]
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

           
        }
    }
}

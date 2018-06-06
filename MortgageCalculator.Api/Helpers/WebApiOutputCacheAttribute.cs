using System;
using System.Web.Http.Filters;
using System.Net.Http.Headers;
using System.Diagnostics.CodeAnalysis;

namespace MortgageCalculator.Api.Helpers
{
    //Caching
    public class WebApiOutputCacheAttribute : ActionFilterAttribute
    {
        public int Duration { get; set; }

        [ExcludeFromCodeCoverage]
        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
        {
            filterContext.Response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(Duration),
                MustRevalidate = true,
                Private = true
            };
        }
    }
}
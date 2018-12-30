using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Emerald
{
    /// <summary>
    /// The sample app demonstrates how to register a middleware with IStartupFilter. 
    /// The sample app includes a middleware that sets an options value from a query string parameter:
    /// </summary>
    public class RequestSetOptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private IOptions<AppOptions> _injectedOptions;

        public RequestSetOptionsMiddleware(
            RequestDelegate next, IOptions<AppOptions> injectedOptions)
        {
            _next = next;
            _injectedOptions = injectedOptions;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine("RequestSetOptionsMiddleware.Invoke");
            var options = httpContext.Request.Query["option"];
            if (!string.IsNullOrWhiteSpace(options))
            {
                _injectedOptions.Value.Option = WebUtility.HtmlEncode(options);
            }

            await _next(httpContext); //next http handler
        }
    }
}

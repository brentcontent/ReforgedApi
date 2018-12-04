using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ReforgedApi.Configuration.Handlers
{
    public class ApiKeyHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // check for swagger
            if (request.RequestUri.AbsolutePath.Contains("swagger"))
            {
                return await base.SendAsync(request, cancellationToken);
            }
            
            IEnumerable<string> apiKeyHeaderValues = null;

            var apiKeyExists = request.Headers.TryGetValues("x-apikey", out apiKeyHeaderValues);
            var apikey = "ditisdeapikey"; // moet normaal in db zitten, en moeilijk zijn :-)

            if (apiKeyExists)
            {
                if (apiKeyHeaderValues.FirstOrDefault().Equals(apikey))
                {
                    return await base.SendAsync(request, cancellationToken);
                }
                
            }

            return request.CreateResponse(System.Net.HttpStatusCode.Forbidden, "Bad API Key");
        }
    }
}
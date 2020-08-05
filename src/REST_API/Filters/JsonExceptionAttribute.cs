using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using REST_API.Exceptions;

namespace REST_API.Filters
{
    /// <summary>
    /// Extends Http response with json exception message.
    /// </summary>
    public class JsonExceptionAttribute : TypeFilterAttribute
    {
        public JsonExceptionAttribute() : base(typeof(HttpCustomExceptionFilterImpl)) {}

        private class HttpCustomExceptionFilterImpl : IExceptionFilter
        {
            private readonly IWebHostEnvironment _environment;
            private readonly ILogger<HttpCustomExceptionFilterImpl> _logger;

            public HttpCustomExceptionFilterImpl(IWebHostEnvironment environment, ILogger<HttpCustomExceptionFilterImpl> logger)
            {
                _environment = environment;
                _logger = logger;
            }

            public void OnException(ExceptionContext context)
            {
                var eventId = new EventId(context.Exception.HResult);
                _logger.LogError(eventId, context.Exception, context.Exception.Message);
                var json = new JsonErrorPayload{EventId = eventId.Id};
                if (_environment.IsDevelopment())
                {
                    json.DetailedMessage = context.Exception.Message;
                }

                const int status = (int) HttpStatusCode.InternalServerError;
                var exceptionObject = new ObjectResult(json) {StatusCode = status};
                context.Result = exceptionObject;
                context.HttpContext.Response.StatusCode = status;
            }

        }
    }
}

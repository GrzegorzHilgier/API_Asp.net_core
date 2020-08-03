using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace REST_API.Middleware
{
    public class ResponseTimeMiddlewareAsync
    {
        private const string X_RESPONSE_TIME_MS = "X-Response-Time-ms";
        private readonly RequestDelegate _next;

        public ResponseTimeMiddlewareAsync(RequestDelegate next)
        {
            _next = next;
        }

        public Task InvokeAsync(HttpContext context)
        {
            var watch = new Stopwatch();
            watch.Start();
            context.Response.OnStarting(() =>
            {
                watch.Stop();
                var responseTime = watch.ElapsedMilliseconds;
                context.Response.Headers[X_RESPONSE_TIME_MS] = responseTime.ToString();
                return Task.CompletedTask;
            });
            return _next(context);
        }
    }
}

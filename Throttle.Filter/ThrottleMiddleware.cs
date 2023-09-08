using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Services.Factory.Interface;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Throttle.Filter
{
    public class ThrottleMiddleware
    {
        private Throttler _throttler;
        private string _throttleGroup;
        private readonly RequestDelegate _next;
        private readonly ILogger<ThrottleMiddleware> logger;
        //private readonly IBusServiceFactory _iBusServiceFactory;

        public ThrottleMiddleware(RequestDelegate next,ILoggerFactory loggerFactory, [CallerMemberName] string ThrottleGroup = "identity")
        {
            _next = next;
            logger = loggerFactory?.CreateLogger<ThrottleMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
            _throttleGroup = ThrottleGroup;
            _throttler = new Throttler(ThrottleGroup);
            //_iBusServiceFactory = iBusServiceFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await setIdentityAsThrottleGroup(context);

            if (_throttler.RequestShouldBeThrottled)
            {
                context.Response.Clear();
                context.Response.StatusCode = Convert.ToInt32((HttpStatusCode)429);
                context.Response.ContentType = "Too many calls! We can only allow " + _throttler.RequestLimit + "  per " + _throttler._timeoutInSeconds;
              
                addThrottleHeaders(context.Response);
            }
            _throttler.IncrementRequestCount();
            addThrottleHeaders(context.Response);

            await _next(context);
        }

        private async Task setIdentityAsThrottleGroup(HttpContext context)
        {
            if (_throttleGroup == "identity")
            {
                var ss= context.User.Identity.Name;
                _throttler.ThrottleGroup = "Siddaroodh";// context.User.Identity.Name;

                //var throttle = _iBusServiceFactory.UserService().GetUserThrottle(_throttler.ThrottleGroup);
                ////var throttle = Ibus.GetUserThrottle(_throttler.ThrottleGroup);
                //_throttler.RequestLimit = throttle.RequestLimit;
                //_throttler._timeoutInSeconds = throttle.TimeoutInSeconds;

                _throttler.RequestLimit = 5;
                _throttler._timeoutInSeconds = 5;
            }

            if (_throttleGroup == "ipaddress")
                _throttler.ThrottleGroup = Convert.ToString(context.Connection.RemoteIpAddress);
        }

        private void addThrottleHeaders(HttpResponse response)
        {
            if (response == null) return;

            foreach (var header in _throttler.GetRateLimitHeaders())
                response.Headers.Add(header.Key, header.Value);
        }
    }
}


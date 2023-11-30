using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Models.DTO;
using Services.Factory.Interface;

using System.Runtime.CompilerServices;
using System.Web.Http.Results;

namespace Throttle.Filter
{
    //[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ThrottleFilter : ActionFilterAttribute
    {
        private Throttler _throttler;
        private string _throttleGroup;
        private readonly IBusServiceFactory _iBusServiceFactory;

        public ThrottleFilter(IBusServiceFactoryResolver iBusServiceFactoryResolver, [CallerMemberName] string ThrottleGroup = null)
        {
            _throttleGroup = ThrottleGroup;
            _throttler = new Throttler(ThrottleGroup);
            this._iBusServiceFactory = iBusServiceFactoryResolver("mssql");
        }
       
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            setIdentityAsThrottleGroup(actionContext.HttpContext);
            if (_throttler.ThrottleGroup != null && _throttler.RequestShouldBeThrottled)
            {
                actionContext.Result = new JsonResult(new OperationResult<string>("", false, "429", "Too many calls! We can only allow " + _throttler.RequestLimit + "  per " + _throttler._timeoutInSeconds));
                //actionContext.Result = new ContentResult
                //{
                //    Content = "Too many calls! We can only allow " + _throttler.RequestLimit + "  per " + _throttler._timeoutInSeconds,
                //    StatusCode = 429, // Too Many Requests
                //    ContentType = "text/plain"
                //};
                addThrottleHeaders(actionContext.HttpContext.Response);
            }

            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            
            setIdentityAsThrottleGroup(actionExecutedContext.HttpContext);
            if (_throttler.ThrottleGroup != null && actionExecutedContext.Exception == null)
            {
                _throttler.IncrementRequestCount();
            }
            addThrottleHeaders(actionExecutedContext.HttpContext.Response);
            base.OnActionExecuted(actionExecutedContext);
        }

        private void setIdentityAsThrottleGroup(HttpContext actionContext)
        {
            if (_throttleGroup == "identity")
            {
                _throttler.ThrottleGroup = actionContext.User.Claims.FirstOrDefault().Value;
                var throttle = _iBusServiceFactory.ConfigurationService().GetUserThrottle(_throttler.ThrottleGroup);
                _throttler.RequestLimit = throttle.RequestLimit;
                _throttler._timeoutInSeconds = throttle.TimeoutInSeconds;
            }

            if (_throttleGroup == "ipaddress")
                _throttler.ThrottleGroup = actionContext.Connection.RemoteIpAddress.ToString();
        }

        private void addThrottleHeaders(HttpResponse response)
        {
            if (response == null) return;

            foreach (var header in _throttler.GetRateLimitHeaders())
                response.Headers.TryAdd(header.Key, header.Value);
        }

    }
}

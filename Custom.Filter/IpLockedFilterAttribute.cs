using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Services.Common.Interface;
using Services.Factory.Interface;
using System;
using System.Net;
using System.Security.Claims;

namespace Custom.Filter
{
    public class IpLockedFilterAttribute : ActionFilterAttribute
    {
        private readonly IBusServiceFactory _iBusServiceFactory;
        private readonly ICacheService _memoryCache;
        private readonly IHttpContextAccessor httpContextAccessor;
        public IpLockedFilterAttribute(IBusServiceFactoryResolver iBusServiceFactoryResolver, ICacheService memoryCache, IHttpContextAccessor httpContextAccessor)
        {
            this._iBusServiceFactory = iBusServiceFactoryResolver("mssql");
            _memoryCache = memoryCache;
            this.httpContextAccessor = httpContextAccessor;
        }

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var context = actionContext.HttpContext;
            var ip = GetClientIpAddress(actionContext.HttpContext);
       
            if (DateTime.Compare(DateTime.Now, _iBusServiceFactory.ConfigurationService().IsIpAddressLocked(ip, 1)[0].ReleaseTime) < 1)
            {
                actionContext.Result = new ContentResult
                {
                    Content = "401 Unauthorized HTTP ",
                    StatusCode = 401, // Too Many Requests
                    ContentType = "text/plain"
                };
            }

            base.OnActionExecuting(actionContext);
        }

        private string GetClientIpAddress(HttpContext request)
        {
            string Ip = "";
            Ip = httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
            if (Ip == "::1")
            {
                var lan = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(r => r.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                Ip = lan == null ? string.Empty : lan.ToString();

                // Ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[2].ToString();
            }
            return Ip;

        }

    }
}

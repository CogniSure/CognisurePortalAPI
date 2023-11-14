using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Services.Common.Interface;
using Services.Factory.Interface;
using System.Net;
using System.Security.Claims;

namespace Custom.Filter
{
    public class CustomFilterAttribute : ActionFilterAttribute
    {
        private readonly IBusServiceFactory _iBusServiceFactory;
        private readonly ICacheService _memoryCache;
        private readonly IHttpContextAccessor httpContextAccessor;
        public CustomFilterAttribute(IBusServiceFactoryResolver iBusServiceFactoryResolver, ICacheService memoryCache, IHttpContextAccessor httpContextAccessor)
        {
            this._iBusServiceFactory = iBusServiceFactoryResolver("mssql");
            _memoryCache = memoryCache;
            this.httpContextAccessor = httpContextAccessor;
        }

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var context = actionContext.HttpContext;
            var ip = GetClientIpAddress(actionContext.HttpContext);
            var action = actionContext.ActionDescriptor.RouteValues["controller"];
            var controller = actionContext.ActionDescriptor.RouteValues["action"];
            string email = string.Format("{0}", actionContext.HttpContext.User.Claims.FirstOrDefault().Value);
            string userid = string.Format("{0}", actionContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var authorizationHeader = string.Format("{0}", actionContext.HttpContext.Request.Headers["Authorization"]);
            string accessToken = "";
            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                var parts = authorizationHeader.ToString().Split(' ');

                if (parts.Length == 2 && parts[0].Equals("Bearer", StringComparison.OrdinalIgnoreCase))
                {
                    accessToken = parts[1];

                    // Use the access token as needed
                }
            }
            var cacheblacklisttoken = _memoryCache.GetData<List<string>>($"UserEmail_BlacklistToken_{email}");

            if (cacheblacklisttoken != null && cacheblacklisttoken.FirstOrDefault(x => x.Equals(accessToken))!=null)
            {
                actionContext.Result = new ContentResult
                {
                    Content = "401 Unauthorized HTTP ",
                    StatusCode = 401, // Too Many Requests
                    ContentType = "text/plain"
                };
            }

            _iBusServiceFactory.ConfigurationService().AddApiLog(email, action, controller, ip, "", actionContext.HttpContext.Request.Method);

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
            }
            return Ip;

        }

    }
}

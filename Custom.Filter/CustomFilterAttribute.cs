using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Services.Factory.Interface;
using System.Security.Claims;

namespace Custom.Filter
{
    public class CustomFilterAttribute : ActionFilterAttribute
    {
        private readonly IBusServiceFactory _iBusServiceFactory;
        private readonly IMemoryCache _memoryCache;
        public CustomFilterAttribute(IBusServiceFactoryResolver iBusServiceFactoryResolver, IMemoryCache memoryCache)
        {
            this._iBusServiceFactory = iBusServiceFactoryResolver("mssql");
            _memoryCache = memoryCache;
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
            //string accessToken = "";
            //if (!string.IsNullOrEmpty(authorizationHeader))
            //{
            //    var parts = authorizationHeader.ToString().Split(' ');

            //    if (parts.Length == 2 && parts[0].Equals("Bearer", StringComparison.OrdinalIgnoreCase))
            //    {
            //        accessToken = parts[1];

            //        // Use the access token as needed
            //    }
            //}
            var cacheblacklisttoken = _memoryCache.Get<List<string>>($"UserEmail_BlacklistToken_{email}");

            //if (string.Format("{0}", cacheblacklisttoken).Contains(authorizationHeader))
            if (cacheblacklisttoken != null && cacheblacklisttoken.FirstOrDefault(x => x.Equals(authorizationHeader))!=null)
            {
                actionContext.Result = new ContentResult
                {
                    Content = "401 Unauthorized HTTP ",
                    StatusCode = 401, // Too Many Requests
                    ContentType = "text/plain"
                };
            }

            _iBusServiceFactory.ConfigurationService().AddApiLog(email, action, controller, ip, "", actionContext.HttpContext.Request.Method);

            //if (!_iBusServiceFactory.ConfigurationService().IsAllowed(email, action, controller))
            //{
            //    actionContext.Result = new ContentResult
            //    {
            //        Content = "UnAuthorized access ",
            //        StatusCode = 302, // Too Many Requests
            //        ContentType = "text/plain"
            //    };
            //}

            base.OnActionExecuting(actionContext);
        }

        private string GetClientIpAddress(HttpContext request)
        {
            var ipAddress = request.Connection.RemoteIpAddress;
            return string.Format("{0}", ipAddress);

            //if (request.Properties.ContainsKey("MS_HttpContext"))
            //{
            //    //return IPAddress.Parse((request.Properties["MS_HttpContext"]).Request.UserHostAddress).ToString();
            //    return IPAddress.Parse(_httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString()).ToString();
            //}
            //if (request.Properties.ContainsKey("MS_OwinContext"))
            //{
            //    return IPAddress.Parse(((OwinContext)request.Properties["MS_OwinContext"]).Request.RemoteIpAddress).ToString();
            //}

        }

    }
}

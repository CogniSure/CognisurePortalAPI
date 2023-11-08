using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Services.Common.Interface;
using Services.Factory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Custom.Filter
{
    public class ControlActionIsUserAllowedFilterAttribute : ActionFilterAttribute
    {
        private readonly IBusServiceFactory _iBusServiceFactory;
        private readonly ICacheService _memoryCache;
        private readonly IHttpContextAccessor httpContextAccessor;
        public ControlActionIsUserAllowedFilterAttribute(IBusServiceFactoryResolver iBusServiceFactoryResolver, ICacheService memoryCache, IHttpContextAccessor httpContextAccessor)
        {
            this._iBusServiceFactory = iBusServiceFactoryResolver("mssql");
            _memoryCache = memoryCache;
            this.httpContextAccessor = httpContextAccessor;
        }

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var action = actionContext.ActionDescriptor.RouteValues["controller"];
            var controller = actionContext.ActionDescriptor.RouteValues["action"];
            string email = string.Format("{0}", actionContext.HttpContext.User.Claims.FirstOrDefault().Value);

            if (!_iBusServiceFactory.ConfigurationService().IsAllowed(email, action, controller))
            {
                actionContext.Result = new ContentResult
                {
                    Content = "UnAuthorized access ",
                    StatusCode = 302, // Too Many Requests
                    ContentType = "text/plain"
                };
            }

            base.OnActionExecuting(actionContext);
        }
    }
}

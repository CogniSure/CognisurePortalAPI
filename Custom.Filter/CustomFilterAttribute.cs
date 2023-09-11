using Microsoft.AspNetCore.Http;
using Microsoft.Owin;
using Services.Factory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Custom.Filter
{
    public class CustomFilterAttribute:  ActionFilterAttribute
    {
        private readonly IBusServiceFactory _iBusServiceFactory;
        public CustomFilterAttribute(IBusServiceFactoryResolver iBusServiceFactoryResolver)
        {
            this._iBusServiceFactory = iBusServiceFactoryResolver("mssql");
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var context = actionContext.RequestContext;
            var ip = GetClientIpAddress(actionContext.Request);
            var action = actionContext.ActionDescriptor.ActionName;
            var controller = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string email = actionContext.ControllerContext.RequestContext.Principal.Identity.Name;

            _iBusServiceFactory.ConfigurationService().AddApiLog(email, action, controller,ip, "", actionContext.Request.Method.Method);

            if (!_iBusServiceFactory.ConfigurationService().IsAllowed(email, action, controller))
            {
                var response = actionContext.Request.CreateErrorResponse(HttpStatusCode.PreconditionFailed, "Permission missed");
                actionContext.Response = response;
            }

            base.OnActionExecuting(actionContext);
        }
        private string GetClientIpAddress(HttpRequestMessage request)
        {
            //if (request.Properties.ContainsKey("MS_HttpContext"))
            //{
            //    return IPAddress.Parse((request.Properties["MS_HttpContext"]).Request.UserHostAddress).ToString();
            //}
            if (request.Properties.ContainsKey("MS_OwinContext"))
            {
                return IPAddress.Parse(((OwinContext)request.Properties["MS_OwinContext"]).Request.RemoteIpAddress).ToString();
            }
            return String.Empty;
        }

    }
}

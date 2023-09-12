using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Owin;
using Microsoft.Win32;
using Models.DTO;
using Services.Factory.Interface;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace Custom.Filter
{
    public class CustomFilterAttribute: System.Web.Http.Filters.ActionFilterAttribute,IFilterMetadata
    {
        private readonly IBusServiceFactory _iBusServiceFactory;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        public CustomFilterAttribute(IBusServiceFactoryResolver iBusServiceFactoryResolver)
        {
            this._iBusServiceFactory = iBusServiceFactoryResolver("mssql");
           // _httpContextAccessor=httpContextAccessor;
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
            //    //return IPAddress.Parse((request.Properties["MS_HttpContext"]).Request.UserHostAddress).ToString();
            //    return IPAddress.Parse(_httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString()).ToString();
            //}
            if (request.Properties.ContainsKey("MS_OwinContext"))
            {
                return IPAddress.Parse(((OwinContext)request.Properties["MS_OwinContext"]).Request.RemoteIpAddress).ToString();
            }
            return String.Empty;
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Services.Factory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Custom.Filter
{
    public class CaptureAPILogs : ActionFilterAttribute
    {
        private readonly IBusServiceFactory _iBusServiceFactory;
        private readonly IHttpContextAccessor httpContextAccessor;
        public CaptureAPILogs(IBusServiceFactoryResolver iBusServiceFactoryResolver, IHttpContextAccessor httpContextAccessor)
        {
            this._iBusServiceFactory = iBusServiceFactoryResolver("mssql");
            this.httpContextAccessor = httpContextAccessor;
        }

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var ip = GetClientIpAddress(actionContext.HttpContext);
            var action = actionContext.ActionDescriptor.RouteValues["controller"];
            var controller = actionContext.ActionDescriptor.RouteValues["action"];
            string email = actionContext.HttpContext.User.Claims.FirstOrDefault().Value;

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

                // Ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[2].ToString();
            }
            return Ip;

        }
    }
}

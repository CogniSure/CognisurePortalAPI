using Microsoft.AspNetCore.Http;
using Services.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class IpAddressServices:IIpAddressServices
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public IpAddressServices(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string? GetIpAddress()
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

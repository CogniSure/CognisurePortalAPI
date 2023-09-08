using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Throttle.Filter
{
    public static class ThrottleMiddlewareExtensions
    {
        public static IApplicationBuilder UseThrottleMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ThrottleMiddleware>();
        }
    }

}

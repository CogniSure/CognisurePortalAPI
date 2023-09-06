using Microsoft.AspNetCore.Builder;

namespace Throttle.Filter
{
    public static class ThrottleFilterMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpStatusCodeExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ThrottleFilterMiddleware>();
        }
    }
}

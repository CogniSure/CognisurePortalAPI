using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Global.Errorhandling
{
    public class HttpExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<HttpExceptionMiddleware> logger;

        public HttpExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            logger = loggerFactory?.CreateLogger<HttpExceptionMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (HttpException ex)
            {
                if (context.Response.HasStarted)
                {
                    logger.LogWarning("The response has already started, the http status code middleware will not be executed.");
                    throw;
                }

                context.Response.Clear();
                context.Response.StatusCode = ex.StatusCode;
                context.Response.ContentType = ex.ContentType;
                await context.Response.WriteAsync(ex.Json != null ? ex.Json.ToString() : ex.Message);

                logger.LogError(ex.InnerException, ex.Message);
            }
            catch (Exception ex)

            {
                if (context.Response.HasStarted)
                {
                    logger.LogWarning("The response has already started, the http status code middleware will not be executed.");
                    throw;
                }

                context.Response.Clear();
                context.Response.StatusCode = ex.HResult;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(ex.Message);

                logger.LogError(ex, ex.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Services.Common.Interface;
using Services.Factory.Interface;
using System.Diagnostics;

namespace Global.Errorhandling
{
    public class HttpExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<HttpExceptionMiddleware> logger;
        //private readonly IBusServiceFactory iBusServiceFactory;
        

        public HttpExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            logger = loggerFactory?.CreateLogger<HttpExceptionMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
           // this.iBusServiceFactory = iBusServiceFactoryResolver("mssql");
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
                //AddError(string hresult, string innerexception, string message, string source, string stacktrace, string targetsite, string addedby);

                //await iBusServiceFactory.ExceptionService<HttpContext>().AddError("",Convert.ToString(ex.InnerException),ex.Message,ex.Source,ex.StackTrace, Convert.ToString(ex.TargetSite),"0","Global","Global");
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
               // await iBusServiceFactory.ExceptionService<HttpContext>().AddError("", Convert.ToString(ex.InnerException), ex.Message, ex.Source, ex.StackTrace, Convert.ToString(ex.TargetSite), "0", "Global", "Global");
                logger.LogError(ex, ex.Message);
            }
        }
    }
}

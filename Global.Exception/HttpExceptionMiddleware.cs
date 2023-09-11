using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Models.DTO;
using Services.Common.Interface;
using Services.Factory.Interface;
using System;
using System.Diagnostics;

namespace Global.Errorhandling
{
    public class HttpExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<HttpExceptionMiddleware> logger;
        private readonly IExceptionService ExceptionDB;


        public HttpExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IExceptionService exceptionDB)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            logger = loggerFactory?.CreateLogger<HttpExceptionMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
            ExceptionDB = exceptionDB;
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
                logger.LogError("Error: {0}", ex.Message);
                await ExceptionDB.AddError("", string.Format("{0}", ex.InnerException), ex.Message, string.Format("{0}", ex.Source), string.Format("{0}", ex.StackTrace), string.Format("{0}", ex.TargetSite),
                    "0", "Global", "Global");
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
                logger.LogError("Error: {0}", ex.Message);
                await ExceptionDB.AddError("", string.Format("{0}", ex.InnerException), ex.Message, string.Format("{0}", ex.Source), string.Format("{0}", ex.StackTrace), string.Format("{0}", ex.TargetSite),
                    "0", "Global", "Global");
            }
        }
    }
}

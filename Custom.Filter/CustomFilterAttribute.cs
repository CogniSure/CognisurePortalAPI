using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Models.DTO;
using Services.Common.Interface;
using Services.Factory.Interface;
using System.Net;
using System.Security.Claims;

namespace Custom.Filter
{
    public class CustomFilterAttribute : ActionFilterAttribute
    {
        private readonly ICacheService _memoryCache;
        public CustomFilterAttribute(ICacheService memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            string email = actionContext.HttpContext.User.Claims.FirstOrDefault().Value;
            string authorizationHeader = string.Format("{0}", actionContext.HttpContext.Request.Headers["Authorization"]);
            string accessToken = "";
            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                var parts = authorizationHeader.ToString().Split(' ');

                if (parts.Length == 2 && parts[0].Equals("Bearer", StringComparison.OrdinalIgnoreCase))
                {
                    accessToken = parts[1];
                }
            }
            var cacheblacklisttoken = _memoryCache.GetData<List<string>>($"UserEmail_BlacklistToken_{email}");

            if (cacheblacklisttoken != null && cacheblacklisttoken.FirstOrDefault(x => x.Equals(accessToken))!=null)
            {
                actionContext.Result = new JsonResult(new OperationResult<string>("", false, "401", "Un Authorized"));
                //actionContext.Result = new ContentResult
                //{
                //    Content = "401 Unauthorized HTTP ",
                //    StatusCode = 401, // Too Many Requests
                //    ContentType = "text/plain"
                //};
            }

            base.OnActionExecuting(actionContext);
        }
    }
}

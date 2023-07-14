using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Portal.Repository.Login;
using Services.Repository;
using Services.Repository.Interface;

namespace PortalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class URLController : ControllerBase
    {
        //private readonly ILogger<DashboardController> _logger;
        public IURLRepository _urlRepository;
        private readonly IConfiguration _configuration;


        public URLController( IURLRepository loginRepository, IConfiguration configuration)
        {
            //_logger = logger;
            _urlRepository = loginRepository;
            _configuration = configuration;
        }

        [HttpGet(Name = "URL")]
        [AllowAnonymous]
        public async Task<OperationResult<WidgetConfiguration>> GetURL(long userId,string pageName, string widgetCode, string action)
        {
            try
            {
                //var tokenApi = _configuration.GetSection("TokenApi")?.Value;
                //if (tokenApi == null)
                //{
                //    throw new ArgumentException("Tokoen api url not found");
                //}

                //_logger.LogInformation(tokenApi);
                return await _urlRepository.GetURL(userId, pageName, widgetCode, action);
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error: {0}", ex.Message);
                return null;
            }
        }
    }
}

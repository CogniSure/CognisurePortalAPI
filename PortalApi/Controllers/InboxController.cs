using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using PortalApi.FactoryResolver;
using Services.Factory.Interface;

namespace PortalApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class InboxController : ControllerBase
    {
        private readonly ILogger<InboxController> _logger;
        private readonly IBusServiceFactory iBusServiceFactory;
        private readonly IConfiguration _configuration;


        public InboxController(ILogger<InboxController> logger,
                                IBusServiceFactoryResolver iBusServiceFactoryResolver, IConfiguration configuration)
        {
            _logger = logger;
            this.iBusServiceFactory = iBusServiceFactoryResolver("mssql");
            _configuration = configuration;
        }
        [Route("AllSubmission")]
        [HttpGet]
        public async Task<OperationResult<List<Submission>>> GetAllSubmission(InboxFilter ObjFilter)
        {
            try
            {
                ObjFilter.UserId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault().Value);
                return await iBusServiceFactory.SubmissionInboxService().GetAllSubmission(ObjFilter);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {0}", ex.Message);
                return null;
            }
        }
    }
}

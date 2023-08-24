using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using PortalApi.FactoryResolver;
using Services.Factory.Interface;

namespace PortalApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api")]
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
        [HttpPost]
        public async Task<OperationResult<List<Submission>>> GetAllSubmission()
        {
            try
            {
                InboxFilter ObjFilter = new InboxFilter();
                ObjFilter.UserId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(C=>C.Type=="UserId").Value);
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

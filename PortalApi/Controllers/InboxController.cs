using Custom.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Services.Factory.Interface;
using System.Security.Claims;

namespace PortalApi.Controllers
{
    [ApiController]
    [Authorize]
    [TypeFilter(typeof(CustomFilterAttribute))]
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
                ObjFilter.UserId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                return await iBusServiceFactory.SubmissionInboxService().GetAllSubmission(ObjFilter);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {0}", ex.Message);
                return null;
                // return await iBusServiceFactory.ExceptionService<List<Submission>>().AddError("", Convert.ToString(ex.InnerException), ex.Message, ex.Source, ex.StackTrace, Convert.ToString(ex.TargetSite),
                //"0", "InboxController", "GetAllSubmission");
            }
        }
    }
}

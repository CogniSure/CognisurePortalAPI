using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Newtonsoft.Json.Linq;
using PortalApi.FactoryResolver;
using Services.Factory.Interface;

namespace PortalApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ILogger<ChatController> _logger;
        private readonly IBusServiceFactory iBusServiceFactory;
        private readonly IConfiguration _configuration;


        public ChatController(ILogger<ChatController> logger,
                                IBusServiceFactoryResolver iBusServiceFactoryResolver, IConfiguration configuration)
        {
            _logger = logger;
            this.iBusServiceFactory = iBusServiceFactoryResolver("api");
            _configuration = configuration;
        }

        [Route("UploadCopilotFIles")]
        [HttpPost]
        public async Task<OperationResult<string>> UploadCopilotFIles(UploadData files)
        {
            try
            {
                List<UploadData> uploadedFile = new List<UploadData>();
                uploadedFile.Add(files);
                return await iBusServiceFactory.ChatService().UploaFiles(uploadedFile);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {0}", ex.Message);
                return null;
            }
        }
        [Route("AskCopilot")]
        [HttpPost]
        public async Task<OperationResult<string>> UploadCopilotFIles(string uniqId,string message)
        {
            try
            {
                return await iBusServiceFactory.ChatService().AskCoPilot(uniqId,message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {0}", ex.Message);
                return null;
            }
        }
    }
}

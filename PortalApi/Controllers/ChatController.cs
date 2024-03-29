﻿using Custom.Filter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Models.DTO;
using Newtonsoft.Json.Linq;
using Services.Factory.Interface;

namespace PortalApi.Controllers
{
    [Route("api")]
    [ApiController]
    [TypeFilter(typeof(CustomFilterAttribute))]
    public class ChatController : ControllerBase
    {
        private readonly ILogger<ChatController> _logger;
        private readonly IBusServiceFactory iBusServiceFactory;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _iMemoryCache;


        public ChatController(ILogger<ChatController> logger,
                                IBusServiceFactoryResolver iBusServiceFactoryResolver, IConfiguration configuration)
        {
            _logger = logger;
            this.iBusServiceFactory = iBusServiceFactoryResolver("api");
            _configuration = configuration;
        }

        [Route("UploadCopilotFiles")]
        [HttpPost]
        
        public async Task<OperationResult<string>> UploadCopilotFIles(UploadData files)
        {
            try
            {
                List<UploadData> uploadedFile = new List<UploadData>();
                uploadedFile.Add(files);
                return await iBusServiceFactory.ChatService().UploadFiles(uploadedFile);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {0}", ex.Message);
                return null;
                //return await iBusServiceFactory.ExceptionService<string>().AddError("", Convert.ToString(ex.InnerException), ex.Message, ex.Source, ex.StackTrace, Convert.ToString(ex.TargetSite),
                //"0", "ChatController", "UploadCopilotFIles");
            }
        }
        [Route("AskCopilot")]
        [HttpGet]
        public async Task<OperationResult<string>> AskCoPilot(string message, string uniqId = "")
        {
            try
            {
                return await iBusServiceFactory.ChatService().AskCoPilot(uniqId,message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {0}", ex.Message);
                return null;
                //return await iBusServiceFactory.ExceptionService<string>().AddError("", Convert.ToString(ex.InnerException), ex.Message, ex.Source, ex.StackTrace, Convert.ToString(ex.TargetSite),
                // "0", "ChatController", "AskCoPilot");
            }
        }
    }
}

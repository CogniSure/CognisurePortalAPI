using Custom.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.DTO;
using Portal.Repository.Inbox;
using Services.Factory.Interface;
using Services.Repository.Interface;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PortalApi.Controllers.Inbox;

[ApiController]
[Route("api")]
[Authorize]
[TypeFilter(typeof(CustomFilterAttribute))]
public class SubmissionController : ControllerBase
{
    
    private readonly ILogger<SubmissionController> _logger;
    public ISubmissionService _inboxRepository;
    private readonly IConfiguration _configuration;
    private readonly IBusServiceFactory iBusServiceFactoryAPI;
    private readonly IBusServiceFactory iBusServiceFactorySFDB;

    public SubmissionController(ILogger<SubmissionController> logger,
        ISubmissionService inboxRepository, IConfiguration configuration,
        IBusServiceFactoryResolver iBusServiceFactoryResolver,
        IHttpContextAccessor contextAccessor)
    {
        _logger = logger;
        _inboxRepository = inboxRepository;
        this.iBusServiceFactoryAPI = iBusServiceFactoryResolver("api");
        this.iBusServiceFactorySFDB = iBusServiceFactoryResolver("sfdb");
        _configuration = configuration;
    }
    [Route("submission")]
    [HttpGet]
    public async Task<OperationResult<SubmissionData>> GetSubmissionById(string submissionid)
    {
        try
        {
            var useremail = HttpContext.User.Claims.FirstOrDefault().Value;
            return await iBusServiceFactoryAPI.SubmissionService().GetSubmissionData(submissionid, useremail);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {0}",ex.Message);
            return null;
            //return await iBusServiceFactorySQL.ExceptionService<SubmissionData>().AddError("", Convert.ToString(ex.InnerException), ex.Message, ex.Source, ex.StackTrace, Convert.ToString(ex.TargetSite),
            //   "0", "SubmissionController", "GetSubmissionById");
        }
    }
    [Route("submissionbyid")]
    [HttpGet]
    [AllowAnonymous]
    public async Task<OperationResult<List<SFResult>>> GetSubmissionById(string type, string clientid,string submissionid, string email)
    //public async Task<OperationResult<List<SFResult>>> GetSubmissionById()
    {
        try
        {
            var Source = _configuration.GetSection("SubSource")?.Value;
            var useremail = HttpContext.User.Claims.FirstOrDefault().Value;
            //string type= "exposure_constructiontype";
            //string clientid = "1074";
            //string submissionid = "a55523ff-1c8e-446a-843f-e51b6a2c4d61";
            //string email = "QBEsub@gmail.com";
            return await iBusServiceFactorySFDB.SubmissionSFService().GetSubmissionData(type, clientid, submissionid, email);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {0}", ex.Message);
            return null;
            //return await iBusServiceFactorySQL.ExceptionService<SubmissionData>().AddError("", Convert.ToString(ex.InnerException), ex.Message, ex.Source, ex.StackTrace, Convert.ToString(ex.TargetSite),
            //   "0", "SubmissionController", "GetSubmissionById");
        }
    }
    [Route("submission360")]
    [HttpGet]
    public async Task<OperationResult<Submission360>> DownloadSubmission360(string submissionid)
    {
        try
        {
            var useremail = HttpContext.User.Claims.FirstOrDefault().Value;
            return await iBusServiceFactoryAPI.SubmissionService().DownloadSubmission360(submissionid, useremail);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {0}", ex.Message);
            return null;
            //return await iBusServiceFactorySQL.ExceptionService<Submission360>().AddError("", Convert.ToString(ex.InnerException), ex.Message, ex.Source, ex.StackTrace, Convert.ToString(ex.TargetSite),
            //    "0", "SubmissionController", "DownloadSubmission360");
        }
    }
}

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
    private readonly IBusServiceFactory iBusServiceFactorySQL;

    public SubmissionController(ILogger<SubmissionController> logger,
        ISubmissionService inboxRepository, IConfiguration configuration,
        IBusServiceFactoryResolver iBusServiceFactoryResolver,
        IHttpContextAccessor contextAccessor)
    {
        _logger = logger;
        _inboxRepository = inboxRepository;
        this.iBusServiceFactoryAPI = iBusServiceFactoryResolver("api");
        this.iBusServiceFactorySFDB = iBusServiceFactoryResolver("sfdb");
        this.iBusServiceFactorySQL = iBusServiceFactoryResolver("mssql");
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
    public async Task<OperationResult<List<DataResult>>> GetSubmissionById(string type, string clientid,string submissionid, string email)
    {
        try
        {
            var Source = _configuration.GetSection("SubSource")?.Value;
            var useremail = HttpContext.User.Claims.FirstOrDefault().Value;
            return await iBusServiceFactorySFDB.SubmissionSFService().GetExposureSummary(type, clientid, submissionid, email);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {0}", ex.Message);
            return null;
        }
    }
    [Route("losssummarybyid")]
    [HttpGet]
    public async Task<OperationResult<List<DataResult>>> GetLossSummaryById(string type, string clientid, string submissionid, string email)
    {
        try
        {
            //type = "loss_incurredbylobbyyear";
            //clientid = "1074";
            //submissionid = "a44413ee-1c8e-446a-843f-e51b6a2c4c51";
            //email = "QBEsub@gmail.com";
            var useremail = HttpContext.User.Claims.FirstOrDefault().Value;
            return await iBusServiceFactorySFDB.SubmissionSFService().GetLossSummary(type, clientid, submissionid, email);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {0}", ex.Message);
            return null;
        }
    }
    [Route("submissionfiles")]
    [HttpGet]
    public async Task<OperationResult<List<SubmissionFile>>> GetSubmissionFiles(string clientid, string submissionid, string email)
    {
        try
        {
            //type = "loss_incurredbylobbyyear";
            //clientid = "1074";
            //submissionid = "a44413ee-1c8e-446a-843f-e51b6a2c4c51";
            //email = "QBEsub@gmail.com";
            var useremail = HttpContext.User.Claims.FirstOrDefault().Value;
            return await iBusServiceFactorySQL.SubmissionService().GetSubmissionFiles(Convert.ToInt32(submissionid), email);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {0}", ex.Message);
            return null;
        }
    }
    [Route("submissionheadersbyid")]
    [HttpGet]
    public async Task<OperationResult<List<DataResult>>> GetSubmissionHeaderId(string type, string clientid, string submissionid, string email)
    {
        try
        {
            //type = "loss_incurredbylobbyyear";
            //clientid = "1074";
            //submissionid = "a44413ee-1c8e-446a-843f-e51b6a2c4c51";
            //email = "QBEsub@gmail.com";
            var useremail = HttpContext.User.Claims.FirstOrDefault().Value;
            return await iBusServiceFactorySFDB.SubmissionSFService().GetSubmissionHeader(type, clientid, submissionid, email);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {0}", ex.Message);
            return null;
        }
    }
    [Route("submissionsummarybylobbyid")]
    [HttpGet]
    public async Task<OperationResult<Submission>> GetSubmissionSummaryForLOB(string type, string clientid, string submissionid, string email)
    {
        try
        {
            //type = "loss_incurredbylobbyyear";
            //clientid = "1074";
            //submissionid = "a44413ee-1c8e-446a-843f-e51b6a2c4c51";
            //email = "QBEsub@gmail.com";
            var useremail = HttpContext.User.Claims.FirstOrDefault().Value;
            return await iBusServiceFactorySFDB.SubmissionSFService().GetSubmissionSummary(type, clientid, submissionid, email);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {0}", ex.Message);
            return null;
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

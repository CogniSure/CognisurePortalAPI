using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.DTO;
using Portal.Repository.Inbox;
using PortalApi.FactoryResolver;
using Services.Factory.Interface;
using Services.Repository.Interface;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PortalApi.Controllers.Inbox;

[ApiController]
[Route("api")]

[Authorize]
public class SubmissionController : ControllerBase
{
    
    private readonly ILogger<SubmissionController> _logger;
    public ISubmissionService _inboxRepository;
    private readonly IConfiguration _configuration;
    private readonly IBusServiceFactory iBusServiceFactory;

    public SubmissionController(ILogger<SubmissionController> logger,
        ISubmissionService inboxRepository, IConfiguration configuration,
        IBusServiceFactoryResolver iBusServiceFactoryResolver,
        IHttpContextAccessor contextAccessor)
    {
        _logger = logger;
        _inboxRepository = inboxRepository;
        this.iBusServiceFactory = iBusServiceFactoryResolver("api");
        _configuration = configuration;
    }
    [Route("submission")]
    [HttpGet]
    public async Task<OperationResult<SubmissionData>> GetSubmissionById(string submissionid)
    {
        try
        {
            var useremail = HttpContext.User.Claims.FirstOrDefault().Value;
            return await iBusServiceFactory.SubmissionService().GetSubmissionData(submissionid, useremail);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {0}",ex.Message);
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
            return await iBusServiceFactory.SubmissionService().DownloadSubmission360(submissionid, useremail);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {0}", ex.Message);
            return null;
        }
    }
}
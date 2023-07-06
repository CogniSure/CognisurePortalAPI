using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Portal.Repository.Inbox;
using System;

namespace PortalApi.Controllers.Inbox;

[ApiController]
[Route("[controller]")]
public class InboxController : ControllerBase
{
    
    private readonly ILogger<InboxController> _logger;
    public IInboxRepository _inboxRepository;
    private readonly IConfiguration _configuration;


    public InboxController(ILogger<InboxController> logger, IInboxRepository inboxRepository, IConfiguration configuration)
    {
        _logger = logger;
        _inboxRepository = inboxRepository;
        _configuration = configuration;
    }

    [HttpGet(Name = "Inbox")]
    public string GetToken()
    {
        try
        {
            var tokenApi = _configuration.GetSection("TokenApi")?.Value;
            if(tokenApi == null) {
                throw new ArgumentException("Tokoen api url not found");
            }

            _logger.LogInformation(tokenApi);
            return _inboxRepository.GetInboxData();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {0}",ex.Message);
            return null;
        }
    }
}

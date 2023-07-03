using Microsoft.AspNetCore.Mvc;
using Portal.Repository.Dashboard;
namespace PortalApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DashboardController : ControllerBase
{
    
    private readonly ILogger<DashboardController> _logger;
    public IDashboardRepository _dashboardRepository;
    private readonly IConfiguration _configuration;
    public DashboardController(ILogger<DashboardController> logger, IDashboardRepository dashboardRepository, IConfiguration configuration)
    {
        _logger = logger;
        _dashboardRepository = dashboardRepository;
        _configuration = configuration;
    }

    [HttpGet(Name = "Dashboard")]
    public string Dashboard()
    {
        try
        {
            var tokenApi = _configuration.GetSection("TokenApi")?.Value;
            if(tokenApi == null) {
                throw new ArgumentException("Tokoen api url not found");
            }

            _logger.LogInformation(tokenApi);
            return _dashboardRepository.GetDashboardData();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {0}",ex.Message);
            return null;
        }
    }
}

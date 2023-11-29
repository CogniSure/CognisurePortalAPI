using Custom.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.DTO;
using Portal.Repository.Dashboard;
using Services.Factory.Interface;
using Services.Repository.Interface;
using System;

namespace PortalApi.Controllers;

[ApiController]
[Route("[controller]")]
//[Authorize]
//[TypeFilter(typeof(CustomFilterAttribute))]
public class DashboardController : ControllerBase
{
    private readonly IBusServiceFactory iBusServiceFactorySFDB;
    private readonly IBusServiceFactory iBusServiceFactoryMSSQL;
    private readonly ILogger<DashboardController> _logger;
    public IDashboardService _dashboardService;
    private readonly IConfiguration _configuration;
    public DashboardController(ILogger<DashboardController> logger, 
        IDashboardService dashboardService, 
        IConfiguration configuration,
        IBusServiceFactoryResolver iBusServiceFactoryResolver)
    {
        _logger = logger;
        _dashboardService = dashboardService;
        _configuration = configuration;
        this.iBusServiceFactorySFDB = iBusServiceFactoryResolver("sfdb");
        this.iBusServiceFactoryMSSQL = iBusServiceFactoryResolver("mssql");
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
            return "Dashboard Data";
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {0}",ex.Message);
            return null;
        }
    }

    [Route("DashboardGraph")]
    [HttpGet]
    public async Task<OperationResult<List<DashboardGraph>>> DashboardGraph([FromQuery]DashboardFilter dashboardFilter, string Type)
    {
        try
        {
            return await iBusServiceFactorySFDB.DashboardService().GetDashboardGraph(dashboardFilter,Type);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {0}", ex.Message);
            return null;
        }
    }
}

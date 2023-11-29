using Custom.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Services.Factory.Interface;
using SnowFlakeAdapter.Interface;
using System.Data;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PortalApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class SFController : ControllerBase
    {
        private readonly ILogger<InboxController> _logger;
        private readonly IBusServiceFactory iBusServiceFactorySFDB;
        private readonly IBusServiceFactory iBusServiceFactoryMSSQL;
        private readonly ISnowFlakeDatabase _SnowFlakeDatabase;
        public SFController(ILogger<InboxController> logger, ISnowFlakeDatabase SnowFlakeDatabase, IBusServiceFactoryResolver iBusServiceFactoryResolver)
        {
            _logger = logger;
            _SnowFlakeDatabase = SnowFlakeDatabase;
            this.iBusServiceFactorySFDB = iBusServiceFactoryResolver("sfdb");
            this.iBusServiceFactoryMSSQL = iBusServiceFactoryResolver("mssql");
        }
        [Route("DashboardGraph")]
        [HttpGet]
        public async Task<OperationResult<List<DashboardGraph>>> DashboardGraph(DashboardFilter objDashboard, string Type)
        {
            try
            {
                return await iBusServiceFactorySFDB.DashboardService().GetDashboardGraph(objDashboard, Type);
                var Ds= _SnowFlakeDatabase.DashboardGraph(Type);
                //return new OperationResult<List<DashboardGraph>>(Ds, true);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {0}", ex.Message);
                return null;
            }
        }
    }
}

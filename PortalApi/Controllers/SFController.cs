using Custom.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Services.Factory.Interface;
using SnowFlakeAdapter.Interface;
using System.Data;
using System.Security.Claims;

namespace PortalApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class SFController : ControllerBase
    {
        private readonly ILogger<InboxController> _logger;
        private readonly ISnowFlakeDatabase _SnowFlakeDatabase;
        public SFController(ILogger<InboxController> logger, ISnowFlakeDatabase SnowFlakeDatabase)
        {
            _logger = logger;
            _SnowFlakeDatabase = SnowFlakeDatabase;
        }
        [Route("Test")]
        [HttpGet]
        public async Task<OperationResult<List<DataSet>>> Test()
        {
            try
            {
                DataSet Ds = new DataSet();
                Ds = _SnowFlakeDatabase.SampleTest();
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

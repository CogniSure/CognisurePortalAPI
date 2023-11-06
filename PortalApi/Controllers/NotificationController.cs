using Custom.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PortalApi.HubConfig;

namespace PortalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [TypeFilter(typeof(CustomFilterAttribute))]
    public class NotificationController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _hub;
        private readonly TimerManager _timer;

        public NotificationController(IHubContext<NotificationHub> hub, TimerManager timer)
        {
            _hub = hub;
            _timer = timer;
        }
        [HttpGet]
        public IActionResult Get()
        {
            if (!_timer.IsTimerStarted)
                _timer.PrepareTimer(() => _hub.Clients.All.SendAsync("TransferChartData", DataManager.GetData()));
            return Ok(new { Message = "Request Completed" });
        }
    }
}

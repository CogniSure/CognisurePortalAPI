using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common.Interface
{
    public interface IConfigurationService
    {
        public ThrottleData GetUserThrottle(string ThrottleGroup);
        public Task<bool> AddApiLog(string email, string actionName, string controllerName,string ipAddress, string sessionID, string requestType);
        public bool IsAllowed(string email, string actionName, string controllerName);
    }
}

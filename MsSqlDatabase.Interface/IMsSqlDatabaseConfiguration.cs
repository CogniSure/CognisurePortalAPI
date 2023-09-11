using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlAdapter.Interface
{
    public interface IMsSqlDatabaseConfiguration
    {
        DataSet GetUserThrottle(string email);
        public Task<bool> AddApiLog(string email, string actionName, string controllerName, string ipAddress, string sessionID, string requestType);
        public bool IsAllowed(string email, string actionName, string controllerName);
    }
}

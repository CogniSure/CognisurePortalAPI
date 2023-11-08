using MsSqlAdapter.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlAdapter
{
    public class MsSqlDatabaseConfiguration: IMsSqlDatabaseConfiguration
    {
        public IMsSqlBaseDatabase BaseDatabase { get; }

        public MsSqlDatabaseConfiguration(IMsSqlBaseDatabase baseDatabase) : base()
        {
            BaseDatabase = baseDatabase;
        }
        public DataSet GetUserThrottle(string email)
        {
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(BaseDatabase.Param("@Email", email));
            return BaseDatabase.GetData("sp_GetUser_Throttle", parameters);
        }
        public async Task<bool> AddApiLog(string email, string actionName, string controllerName,string ipAddress, string sessionID, string requestType)
        {
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(BaseDatabase.Param("@Email", email));
            parameters.Add(BaseDatabase.Param("@actionName", actionName));
            parameters.Add(BaseDatabase.Param("@ControllerName", controllerName));
            parameters.Add(BaseDatabase.Param("@IpAddress", ipAddress));
            parameters.Add(BaseDatabase.Param("@SessionID", sessionID));
            parameters.Add(BaseDatabase.Param("@RequestType", requestType));

            bool result = BaseDatabase.Execute("sp_AddApiLog", parameters);
            return result;
        }
        public bool IsAllowed(string email, string actionName, string controllerName)
        {
            bool result = false;
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(BaseDatabase.Param("@Email", email));
            parameters.Add(BaseDatabase.Param("@ActionName", actionName));
            parameters.Add(BaseDatabase.Param("@ControllerName", controllerName));

            DataSet dst = BaseDatabase.GetData("sp_IsUserAllowed", parameters);

            if (dst != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
            {
                result = true;
            }

            return result;
        }
        public DataSet IsIpAddressLocked(string ipAddress, int ipAddressTypeID)
        {
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(BaseDatabase.Param("@IpAddress", ipAddress));
            parameters.Add(BaseDatabase.Param("@IpAddressTypeID", ipAddressTypeID));
            return BaseDatabase.GetData("sp_IsIpAddressLocked", parameters);
        }
    }
}

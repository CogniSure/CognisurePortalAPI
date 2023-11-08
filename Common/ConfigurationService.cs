using Extention;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.DTO;
using MsSqlAdapter.Interface;
using Services.Common.Interface;
using Services.MsSqlServices.Interface;
using Services.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IMsSqlDatabaseConfiguration _iMsSqlDatabaseConfiguration;
        public IConfiguration Configuration { get; }
        public ConfigurationService(
                  IMsSqlDatabaseConfiguration msSqlDatabaseConfiguration
              )
        {
            this._iMsSqlDatabaseConfiguration = msSqlDatabaseConfiguration;
        }
        public ThrottleData GetUserThrottle(string email)
        {
            var dst = _iMsSqlDatabaseConfiguration.GetUserThrottle(email);

            if (dst != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
            {
                return new ThrottleData
                {
                    RequestLimit = Convert.ToInt32(dst.Tables[0].Rows[0]["ApiRequestLimitPerSecond"]),
                    TimeoutInSeconds = Convert.ToInt32(dst.Tables[0].Rows[0]["ApiTimeoutInSeconds"])
                };
            }
            return new ThrottleData();
        }
        public async Task<bool> AddApiLog(string email, string actionName, string controllerName, string ipAddress, string sessionID, string requestType)
        {
           return await _iMsSqlDatabaseConfiguration.AddApiLog(email, actionName, controllerName, ipAddress, sessionID, requestType);
        }
        public bool IsAllowed(string email, string actionName, string controllerName)
        {
            return _iMsSqlDatabaseConfiguration.IsAllowed(email, actionName, controllerName);
        }
        public List<LoginIpAddress> IsIpAddressLocked(string IpAddress, int IpAddressTypeID)
        {
            return ConvertDataTable<LoginIpAddress>(_iMsSqlDatabaseConfiguration.IsIpAddressLocked(IpAddress, IpAddressTypeID).Tables[0]);
        }
        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}

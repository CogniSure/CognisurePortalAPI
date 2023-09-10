using Extention;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.DTO;
using Services.Common.Interface;
using Services.MsSqlServices.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using Microsoft.Extensions.Caching.Memory;
using System.Runtime.Caching;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Data;
using MsSqlAdapter.Interface;

namespace Common
{
    public class ExceptionService : IExceptionService
    {
        private readonly IMsSqlDatabaseException _msSqlDatabaseException;

        public ExceptionService(
                IMsSqlDatabaseException msSqlDatabaseException
              )
        {
            //this.cacheProvider = cacheProvider;
            this._msSqlDatabaseException = msSqlDatabaseException;
        }

        public async Task<OperationResult<string>> AddError(string hresult, string innerexception, string message, string source, string stacktrace, string targetsite, string addedby, string ControllerName, string ActionName)
        {
            _msSqlDatabaseException.AddError(hresult, innerexception, message, source, stacktrace, targetsite, addedby, ControllerName, ActionName);

            //return new OperationResult<T>((T)Activator.CreateInstance(typeof(T)), false, "500", "Internal Server Error");
            return new OperationResult<string>("", false, "500", "Internal Server Error");
        }
        public async Task<OperationResult<string>> AddError(Exception exe, string addedby, string ControllerName, string ActionName)
        {
            _msSqlDatabaseException.AddError(exe, addedby, ControllerName, ActionName);

            //return new OperationResult<T>((T)Activator.CreateInstance(typeof(T)), false, "500", "Internal Server Error");
            return new OperationResult<string>("", false, "500", "Internal Server Error");
        }
        public async Task<OperationResult<string>> AddError(Exception exe, string ControllerName, string ActionName)
        {
            _msSqlDatabaseException.AddError(exe, ControllerName, ActionName);

            //return new OperationResult<T>((T)Activator.CreateInstance(typeof(T)), false, "500", "Internal Server Error");
            return new OperationResult<string>("", false, "500", "Internal Server Error");
        }
        public async Task<OperationResult<string>> AddError(string errorText, string ControllerName, string ActionName)
        {
            _msSqlDatabaseException.AddError(errorText, ControllerName, ActionName);

            //return new OperationResult<T>((T)Activator.CreateInstance(typeof(T)), false, "500", "Internal Server Error");
            return new OperationResult<string>("", false, "500", "Internal Server Error");
        }

    }
}

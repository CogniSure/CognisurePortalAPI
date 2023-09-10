using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common.Interface
{
    public interface IExceptionService
    {

        Task<OperationResult<string>> AddError(string hresult, string innerexception, string message, string source, string stacktrace, string targetsite, string addedby, string ControllerName, string ActionName);
        Task<OperationResult<string>> AddError(Exception exe, string addedby, string ControllerName, string ActionName);
        Task<OperationResult<string>> AddError(Exception exe, string ControllerName, string ActionName);
        Task<OperationResult<string>> AddError(string errorText, string ControllerName, string ActionName);
    }
}

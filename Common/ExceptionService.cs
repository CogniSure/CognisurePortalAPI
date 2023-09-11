using Services.Common.Interface;
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
            this._msSqlDatabaseException = msSqlDatabaseException;
        }

        public async Task AddError(string hresult, string innerexception, string message, string source, string stacktrace, string targetsite, string addedby, string ControllerName, string ActionName)
        {
            await _msSqlDatabaseException.AddError(hresult, innerexception, message, source, stacktrace, targetsite, addedby, ControllerName, ActionName);
        }
        public async Task AddError(Exception exe, string addedby, string ControllerName, string ActionName)
        {
            await _msSqlDatabaseException.AddError(exe, addedby, ControllerName, ActionName);

        }
        public async Task AddError(Exception exe, string ControllerName, string ActionName)
        {
            await _msSqlDatabaseException.AddError(exe, ControllerName, ActionName);
        }
        public async Task AddError(string errorText, string ControllerName, string ActionName)
        {
            await _msSqlDatabaseException.AddError(errorText, ControllerName, ActionName);
        }
    }
}

namespace MsSqlAdapter.Interface
{
    public interface IMsSqlDatabaseException
    {
        public Task AddError(string hresult, string innerexception, string message, string source, string stacktrace, string targetsite, string addedby, string ControllerName, string ActionName);
        public Task AddError(Exception exe, string addedby, string ControllerName, string ActionName);
        public Task AddError(Exception exe, string ControllerName, string ActionName);
        public Task AddError(string errorText, string ControllerName, string ActionName);
    }
}

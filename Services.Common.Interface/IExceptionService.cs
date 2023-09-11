namespace Services.Common.Interface
{
    public interface IExceptionService
    {

        Task AddError(string hresult, string innerexception, string message, string source, string stacktrace, string targetsite, string addedby, string ControllerName, string ActionName);
        Task AddError(Exception exe, string addedby, string ControllerName, string ActionName);
        Task AddError(Exception exe, string ControllerName, string ActionName);
        Task AddError(string errorText, string ControllerName, string ActionName);
    }
}

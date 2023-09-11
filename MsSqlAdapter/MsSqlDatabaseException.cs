using MsSqlAdapter.Interface;
using System.Data;

namespace MsSqlAdapter
{
    public class MsSqlDatabaseException: IMsSqlDatabaseException
    {
        public IMsSqlBaseDatabase BaseDatabase { get; }

        public MsSqlDatabaseException(IMsSqlBaseDatabase baseDatabase) : base()
        {
            BaseDatabase = baseDatabase;
        }
        public async Task AddError(string hresult, string innerexception, string message
         , string source, string stacktrace, string targetsite, string addedby, string ControllerName, string ActionName)
        {

            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(BaseDatabase.Param("@Hresult", hresult));
            parameters.Add(BaseDatabase.Param("@InnerException", innerexception));
            parameters.Add(BaseDatabase.Param("@Message", message));
            parameters.Add(BaseDatabase.Param("@Source", source));
            parameters.Add(BaseDatabase.Param("@StackTrace", stacktrace));
            parameters.Add(BaseDatabase.Param("@TargetSite", targetsite));
            parameters.Add(BaseDatabase.Param("@AddedBy", addedby));
            parameters.Add(BaseDatabase.Param("@ControllerName", ControllerName));
            parameters.Add(BaseDatabase.Param("@ActionName", ActionName));
            parameters.Add(BaseDatabase.Param("@ErrorReceivedChannel", 2));

            BaseDatabase.Execute("sp_AddError", parameters);

        }

        public async Task AddError(Exception exe, string addedby, string ControllerName, string ActionName)
        {

            await AddError("", string.Format("{0}", exe.InnerException), exe.Message, string.Format("{0}", exe.Source), string.Format("{0}", exe.StackTrace)
                , string.Format("{0}", exe.TargetSite), addedby, ControllerName, ActionName);
        }

        public async Task AddError(Exception exe, string ControllerName, string ActionName)
        {

            await AddError("", string.Format("{0}", exe.InnerException), exe.Message, string.Format("{0}", exe.Source), string.Format("{0}", exe.StackTrace)
                , string.Format("{0}", exe.TargetSite), "", ControllerName, ActionName);
        }

        public async Task AddError(string errorText, string ControllerName, string ActionName)
        {
            await AddError("", "", errorText, "", "", "", "", ControllerName, ActionName);
        }
    }
}

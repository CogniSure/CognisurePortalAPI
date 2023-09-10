using MsSqlAdapter.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlAdapter
{
    public class MsSqlDatabaseException: IMsSqlDatabaseException
    {
        public IMsSqlBaseDatabase BaseDatabase { get; }

        public MsSqlDatabaseException(IMsSqlBaseDatabase baseDatabase) : base()
        {
            BaseDatabase = baseDatabase;
        }
        public void AddError(string hresult, string innerexception, string message
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

        public void AddError(Exception exe, string addedby, string ControllerName, string ActionName)
        {

            AddError("", string.Format("{0}", exe.InnerException), exe.Message, exe.Source, exe.StackTrace
                , exe.TargetSite.ToString(), addedby, ControllerName, ActionName);
        }

        public void AddError(Exception exe, string ControllerName, string ActionName)
        {

            AddError("", string.Format("{0}", exe.InnerException), exe.Message, exe.Source, exe.StackTrace
                , exe.TargetSite.ToString(), "", ControllerName, ActionName);
        }

        public void AddError(string errorText, string ControllerName, string ActionName)
        {
            AddError("", "", errorText, "", "", "", "", ControllerName, ActionName);
        }
    }
}

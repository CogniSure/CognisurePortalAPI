using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlAdapter.Interface
{
    public interface IMsSqlDatabaseException
    {
        public abstract void AddError(string hresult, string innerexception, string message, string source, string stacktrace, string targetsite, string addedby, string ControllerName, string ActionName);
        public abstract void AddError(Exception exe, string addedby, string ControllerName, string ActionName);
        public abstract void AddError(Exception exe, string ControllerName, string ActionName);
        public abstract void AddError(string errorText, string ControllerName, string ActionName);
    }
}

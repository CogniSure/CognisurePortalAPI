using System.Data;

namespace MsSqlDatabase.Interface
{
    public interface IMsSqlBaseDatabase
    {
        IDataParameter Param(string parameterName, object parameterValue);
        DataSet GetData(string storedProcedure, List<IDataParameter> parameters);
        IDataParameter ParamOut(string parameterName, SqlDbType type);
        IDataParameter ParamOut(string parameterName, int size);
        bool Execute(string storedProcedure, List<IDataParameter> parameters, out string generalMessage, out string technicalMessage);
        bool Execute(string storedProcedure, List<IDataParameter> parameters);
        DataSet GetData(string storedProcedure);

    }
}
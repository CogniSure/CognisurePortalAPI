using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFlakeAdapter.Interface
{
    public interface ISnowFlakeBaseDatabase
    {
        IDbDataParameter Param(string parameterName, object parameterValue);
        bool Execute(string storedProcedure, out string generalMessage, out string technicalMessage);
        bool Execute(string storedProcedure);
        DataSet GetData(string storedProcedure, List<IDbDataParameter> parameters);
        DataSet GetData(string storedProcedure);
        DataSet GetValueByQueryText(string queryText);
        Object GetValue(string storedProcedure, out string generalMessage, out string technicalMessage);
        Object GetValueByQueryText(string queryText, out string generalMessage, out string technicalMessage);
    }
}

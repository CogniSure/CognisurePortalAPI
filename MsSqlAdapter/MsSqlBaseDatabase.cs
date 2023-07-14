using Microsoft.Extensions.Configuration;
using MsSqlAdapter.Interface;
using System.Data;
using System.Data.SqlClient;

namespace MsSqlAdapter
{
    public class MsSqlBaseDatabase : IMsSqlBaseDatabase
    {
        public IConfiguration Configuration { get; }
        public string ConnectionString;

        public MsSqlBaseDatabase(IConfiguration Configuration) : base()
        {
            this.Configuration = Configuration;
            ConnectionString = Configuration["ConnectionStrings:SQLConnection"];
        }
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }
        public IDbCommand CreateCommand()
        {
            return new SqlCommand();
        }
        public IDbConnection CreateOpenConnection()
        {
            SqlConnection connection = (SqlConnection)CreateConnection();
            connection.Open();
            return connection;
        }
        public IDbCommand CreateCommand(string commandText, IDbConnection connection)
        {
            SqlCommand command = (SqlCommand)CreateCommand();
            command.CommandText = commandText;
            command.Connection = (SqlConnection)connection;
            command.CommandType = CommandType.Text;
            return command;
        }
        public IDbCommand CreateStoredProcCommand(string procName, IDbConnection connection)
        {
            SqlCommand command = (SqlCommand)CreateCommand();
            command.CommandText = procName;
            command.Connection = (SqlConnection)connection;
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }

        public IDataAdapter CreateDataAdapter()
        {
            return new SqlDataAdapter();
        }

        public IDataAdapter CreateDataAdapter(IDbCommand command)
        {
            return new SqlDataAdapter((SqlCommand)command);
        }

        public IDataAdapter CreateDataAdapter(string commandText, IDbConnection connection)
        {
            return new SqlDataAdapter(commandText, (SqlConnection)connection);
        }

        public IDataAdapter CreateDataAdapter(string commandText, string connection)
        {
            return new SqlDataAdapter(commandText, connection);
        }

        public IDataParameter ParamOut(string parameterName, int size)
        {
            return new SqlParameter(parameterName, SqlDbType.VarChar) { Direction = ParameterDirection.Output, Size = size };
        }

        public IDataParameter ParamInOut(string parameterName, int size, object parameterValue)
        {
            return new SqlParameter(parameterName, SqlDbType.VarChar) { Direction = ParameterDirection.InputOutput, Size = size, Value = parameterValue };
        }

        public IDataParameter ParamOut(string parameterName, SqlDbType type)
        {
            return new SqlParameter(parameterName, type) { Direction = ParameterDirection.Output };
        }

        public IDataParameter Param(string parameterName, object parameterValue)
        {
            return new SqlParameter(parameterName, parameterValue);
        }

        public IDataParameter ParamInt(string parameterName, object parameterValue)
        {
            return new SqlParameter(parameterName, parameterValue) { SqlDbType = SqlDbType.Int }; ;
        }

        public IDataParameter ParamString(string parameterName, object parameterValue)
        {
            return new SqlParameter(parameterName, parameterValue);
        }

        public IDataParameter ParamTable(string parameterName, object parameterValue)
        {
            return new SqlParameter(parameterName, parameterValue) { SqlDbType = SqlDbType.Structured };
        }

        public IDataParameter ParamDecimal(string parameterName, object parameterValue)
        {
            return new SqlParameter(parameterName, parameterValue) { SqlDbType = SqlDbType.Decimal };
        }

        public IDataParameter ParamOut(string parameterName, object parameterValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Connection to database using Store Procedure and List of parameters.
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataSet GetData(string storedProcedure, List<IDataParameter> parameters)
        {
            DataSet ObtainedData = new DataSet();
            try
            {
                using (IDbConnection connection = CreateOpenConnection())
                {
                    using (IDbCommand command = CreateStoredProcCommand(storedProcedure, connection))
                    {
                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                command.Parameters.Add(parameter);
                            }
                        }

                        IDataAdapter dataAdapter = CreateDataAdapter(command);
                        dataAdapter.Fill(ObtainedData);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return ObtainedData;
        }

        public DataSet GetDataByQueryText(string queryText, List<IDataParameter> parameters)
        {
            DataSet ObtainedData = new DataSet();
            try
            {
                using (IDbConnection connection = CreateOpenConnection())
                {
                    using (IDbCommand command = CreateCommand(queryText, connection))
                    {
                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                command.Parameters.Add(parameter);
                            }
                        }

                        IDataAdapter dataAdapter = CreateDataAdapter(command);
                        dataAdapter.Fill(ObtainedData);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return ObtainedData;
        }

        /// <summary>
        /// Connection to database using Store Procedure with no parameters.
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <returns></returns>
        public DataSet GetData(string storedProcedure)
        {
            return GetData(storedProcedure, new List<IDataParameter>());
        }

        public DataSet GetDataByQueryText(string storedProcedure)
        {
            return GetDataByQueryText(storedProcedure, new List<IDataParameter>());
        }

        /// <summary>
        /// Connection to database using Store Procedure with one parameter.
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public DataSet GetData(string storedProcedure, IDataParameter parameter)
        {
            return GetData(storedProcedure, new List<IDataParameter>() { parameter });
        }

        public DataSet GetDataByQueryText(string storedProcedure, IDataParameter parameter)
        {
            return GetDataByQueryText(storedProcedure, new List<IDataParameter>() { parameter });
        }

        //public IDataParameter Param(string parameterName, object parameterValue)
        //{
        //    return Param(parameterName, parameterValue);
        //}

        /// <summary>
        /// Method to Execute the Stored Procedure and get an output message.
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public void Execute(string storedProcedure)
        {
            string generalMessage = "";
            string technicalMessage = "";
            Execute(storedProcedure, new List<IDataParameter>() { }, out generalMessage, out technicalMessage);
        }

        public void ExecuteByQueryText(string queryText)
        {
            string generalMessage = "";
            string technicalMessage = "";
            ExecuteByQueryText(queryText, new List<IDataParameter>() { }, out generalMessage, out technicalMessage);
        }

        /// <summary>
        /// Method to Execute the Stored Procedure with a parameter and get an output message.
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <param name="parameter"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool Execute(string storedProcedure, IDataParameter parameter, out string generalMessage, out string technicalMessage)
        {
            return Execute(storedProcedure, new List<IDataParameter>() { parameter }, out generalMessage, out technicalMessage);
        }

        public bool ExecuteByQueryText(string queryText, IDataParameter parameter, out string generalMessage, out string technicalMessage)
        {
            return ExecuteByQueryText(queryText, new List<IDataParameter>() { parameter }, out generalMessage, out technicalMessage);
        }


        /// <summary>
        /// Method to Execute the Stored Procedure with list of parameters and get an output message.
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <param name="parameters"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool Execute(string storedProcedure, List<IDataParameter> parameters, out string generalMessage, out string technicalMessage)
        {
            bool executionStatus = false;
            generalMessage = "";
            technicalMessage = "";

            try
            {
                using (IDbConnection connection = CreateOpenConnection())
                {
                    using (IDbCommand command = CreateStoredProcCommand(storedProcedure, connection))
                    {
                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                command.Parameters.Add(parameter);
                            }
                        }

                        command.ExecuteNonQuery();
                        executionStatus = true;
                        generalMessage = GetTextValue(command.Parameters, "@GeneralMessage");
                        executionStatus = GetBoolValue(command.Parameters, "@IsSuccess");
                        technicalMessage = GetTextValue(command.Parameters, "@TechnicalMessage");
                    }
                }
            }
            catch (Exception exe)
            {
                generalMessage = "";
                technicalMessage = exe.Message;
                executionStatus = false;
            }

            return executionStatus;
        }

        public bool ExecuteByQueryText(string queryText, List<IDataParameter> parameters, out string generalMessage, out string technicalMessage)
        {
            bool executionStatus = false;
            generalMessage = "";
            technicalMessage = "";

            try
            {
                using (IDbConnection connection = CreateOpenConnection())
                {
                    using (IDbCommand command = CreateCommand(queryText, connection))
                    {
                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                command.Parameters.Add(parameter);
                            }
                        }

                        command.ExecuteNonQuery();
                        executionStatus = true;
                        generalMessage = GetTextValue(command.Parameters, "@GeneralMessage");
                        executionStatus = GetBoolValue(command.Parameters, "@IsSuccess");
                        technicalMessage = GetTextValue(command.Parameters, "@TechnicalMessage");
                    }
                }
            }
            catch (Exception exe)
            {
                generalMessage = "";
                technicalMessage = exe.Message;
                executionStatus = false;
            }

            return executionStatus;
        }

        public bool Execute(string storedProcedure, List<IDataParameter> parameters)
        {
            bool executionStatus = false;

            try
            {
                using (IDbConnection connection = CreateOpenConnection())
                {
                    using (IDbCommand command = CreateStoredProcCommand(storedProcedure, connection))
                    {
                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                command.Parameters.Add(parameter);
                            }
                        }

                        command.ExecuteNonQuery();
                        executionStatus = true;
                    }
                }
            }
            catch (Exception exe)
            {
                return executionStatus;
            }
            return executionStatus;
        }

        public bool ExecuteByQueryText(string queryText, List<IDataParameter> parameters)
        {
            bool executionStatus = false;

            try
            {
                using (IDbConnection connection = CreateOpenConnection())
                {
                    using (IDbCommand command = CreateCommand(queryText, connection))
                    {
                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                command.Parameters.Add(parameter);
                            }
                        }

                        command.ExecuteNonQuery();
                        executionStatus = true;
                    }
                }
            }
            catch (Exception exe)
            {
                return executionStatus;
            }
            return executionStatus;
        }


        public Object GetValue(string storedProcedure, List<IDataParameter> parameters, out string generalMessage, out string technicalMessage)
        {
            Object returnedvalue = new Object();

            bool executionStatus = false;
            generalMessage = "";
            technicalMessage = "";

            try
            {
                using (IDbConnection connection = CreateOpenConnection())
                {
                    using (IDbCommand command = CreateStoredProcCommand(storedProcedure, connection))
                    {
                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                command.Parameters.Add(parameter);
                            }
                        }

                        returnedvalue = command.ExecuteScalar();

                        generalMessage = GetTextValue(command.Parameters, "@GeneralMessage");
                        executionStatus = GetBoolValue(command.Parameters, "@IsSuccess");
                        technicalMessage = GetTextValue(command.Parameters, "@TechnicalMessage");
                    }
                }
            }
            catch (Exception exe)
            {
                generalMessage = "";
                technicalMessage = exe.Message;

                return null;
            }
            return returnedvalue;
        }

        public Object GetValueByQueryText(string queryText, List<IDataParameter> parameters, out string generalMessage, out string technicalMessage)
        {
            Object returnedvalue = new Object();

            bool executionStatus = false;
            generalMessage = "";
            technicalMessage = "";

            try
            {
                using (IDbConnection connection = CreateOpenConnection())
                {
                    using (IDbCommand command = CreateCommand(queryText, connection))
                    {
                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                command.Parameters.Add(parameter);
                            }
                        }

                        returnedvalue = command.ExecuteScalar();

                        generalMessage = GetTextValue(command.Parameters, "@GeneralMessage");
                        executionStatus = GetBoolValue(command.Parameters, "@IsSuccess");
                        technicalMessage = GetTextValue(command.Parameters, "@TechnicalMessage");
                    }
                }
            }
            catch (Exception exe)
            {
                generalMessage = "";
                technicalMessage = exe.Message;

                return null;
            }
            return returnedvalue;
        }


        /// <summary>
        /// Obtain the Scalar value from the Stored Procedure using a parameter.
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <param name="parameter"></param>
        /// <param name="generalMessage"></param>
        /// <param name="technicalMessage"></param>
        /// <returns></returns>
        public Object GetValue(string storedProcedure, IDataParameter parameter, out string generalMessage, out string technicalMessage)
        {
            return GetValue(storedProcedure, new List<IDataParameter>() { parameter }, out generalMessage, out technicalMessage);
        }

        public Object GetValueByQueryText(string queryText, IDataParameter parameter, out string generalMessage, out string technicalMessage)
        {
            return GetValueByQueryText(queryText, new List<IDataParameter>() { parameter }, out generalMessage, out technicalMessage);
        }


        /// <summary>
        /// Obtain the Scalar value from the Stored Procedure.
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <param name="generalMessage"></param>
        /// <param name="technicalMessage"></param>
        /// <returns></returns>
        public Object GetValue(string storedProcedure, out string generalMessage, out string technicalMessage)
        {
            return GetValue(storedProcedure, new List<IDataParameter>() { }, out generalMessage, out technicalMessage);
        }

        public Object GetValueByQueryText(string queryText, out string generalMessage, out string technicalMessage)
        {
            return GetValueByQueryText(queryText, new List<IDataParameter>() { }, out generalMessage, out technicalMessage);
        }
        public string Text(object value)
        {
            return string.Format("{0}", value);
        }

        public string GetTextValue(IDataParameterCollection parameters, string parameterName)
        {
            if (parameters.Contains(parameterName))
                return Text(((IDbDataParameter)parameters[parameterName]).Value);
            else
                return "";
        }

        public bool GetBoolValue(IDataParameterCollection parameters, string parameterName)
        {
            if (parameters.Contains(parameterName))
                return Convert.ToBoolean(((IDbDataParameter)parameters[parameterName]).Value);
            else
                return false;
        }


    }
}
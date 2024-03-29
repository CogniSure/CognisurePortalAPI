﻿using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Snowflake.Data.Client;
using Snowflake.Data.Core;
using SnowFlakeAdapter.Interface;
using System.Data;
using System.Drawing;

namespace SnowFlakeAdapter
{
    public class SnowFlakeBaseDatabase: ISnowFlakeBaseDatabase
    {
        public IConfiguration Configuration { get; }
        public string ConnectionString;

        public SnowFlakeBaseDatabase(IConfiguration Configuration) : base()
        {
            this.Configuration = Configuration;
            ConnectionString = Configuration["ConnectionStrings:SnowFlakeDBConnection"];
        }
        public IDbConnection CreateConnection()
        {
            return new SnowflakeDbConnection(ConnectionString);
        }
        public IDbCommand CreateCommand()
        {
            return new SnowflakeDbCommand();
        }
        public IDbConnection CreateOpenConnection()
        {
            SnowflakeDbConnection connection = (SnowflakeDbConnection)CreateConnection();
            connection.Open();
            return connection;
        }
        public IDbCommand CreateCommand(string commandText, IDbConnection connection)
        {
            SnowflakeDbCommand command = (SnowflakeDbCommand)CreateCommand();
            command.CommandText = commandText;
            command.Connection = (SnowflakeDbConnection)connection;
            command.CommandType = CommandType.Text;
            return command;
        }
        public IDbCommand CreateStoredProcCommand(string procName, IDbConnection connection)
        {
            SnowflakeDbCommand command = (SnowflakeDbCommand)CreateCommand();
            command.CommandText = procName;
            command.Connection = (SnowflakeDbConnection)connection;
            command.CommandType = CommandType.Text;
            return command;
        }

        public IDataAdapter CreateDataAdapter()
        {
            return new SnowflakeDbDataAdapter();
        }

        public IDataAdapter CreateDataAdapter(IDbCommand command)
        {
            return new SnowflakeDbDataAdapter((SnowflakeDbCommand)command);
        }
        public IDataAdapter CreateDataAdapter(string commandText, IDbConnection connection)
        {
            return new SnowflakeDbDataAdapter(commandText, (SnowflakeDbConnection)connection);
        }
        /// <summary>
        /// Method to Execute the Stored Procedure with list of parameters and get an output message.
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <param name="parameters"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public IDataParameter ParamOut(string parameterName, int size)
        {
            return new SnowflakeDbParameter(parameterName, SFDataType.TEXT) { Direction = ParameterDirection.Output, Size = size };
        }
        public IDataParameter ParamInOut(string parameterName, int size, object parameterValue)
        {
            return new SnowflakeDbParameter(parameterName, SFDataType.TEXT) { Direction = ParameterDirection.InputOutput, Size = size, Value = parameterValue };
        }

        public IDataParameter ParamOut(string parameterName, SFDataType type)
        {
            return new SnowflakeDbParameter(parameterName, type) { Direction = ParameterDirection.Output };
        }

        public IDbDataParameter Param(string parameterName, object parameterValue)
        {
            return new SnowflakeDbParameter
            {
                DbType = DbType.String,
                Size = 1,
                ParameterName = parameterName,
                Value= parameterValue
            };
        }

        public IDataParameter ParamInt(string parameterName, object parameterValue)
        {
            return new SnowflakeDbParameter(parameterName, (SFDataType)parameterValue) { SFDataType = SFDataType.TEXT };
        }

        public IDataParameter ParamString(string parameterName, object parameterValue)
        {
            return new SnowflakeDbParameter(parameterName, (SFDataType)parameterValue);
        }

        public IDataParameter ParamTable(string parameterName, object parameterValue)
        {
            return new SnowflakeDbParameter(parameterName, (SFDataType)parameterValue) { SFDataType = SFDataType.OBJECT };
        }

        public IDataParameter ParamDecimal(string parameterName, object parameterValue)
        {
            return new SnowflakeDbParameter(parameterName, (SFDataType)parameterValue) { SFDataType = SFDataType.TEXT };
        }

        public IDataParameter ParamOut(string parameterName, object parameterValue)
        {
            throw new NotImplementedException();
        }
        public bool Execute(string storedProcedure)
        {
            bool executionStatus = false;

            try
            {
                using (IDbConnection connection = CreateOpenConnection())
                {
                    using (IDbCommand command = CreateStoredProcCommand(storedProcedure, connection))
                    {
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
        public bool Execute(string storedProcedure, out string generalMessage, out string technicalMessage)
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
        public DataSet GetData(string storedProcedure, List<IDbDataParameter> parameters)
        {
            DataSet ObtainedData = new DataSet();
            try
            {
                using (IDbConnection connection = CreateOpenConnection())
                {
                    using (var command = connection.CreateCommand())
                    {
                        string jsonParams = "";
                        // Add parameters to the command
                        foreach (var parameter in parameters)
                        {
                            if (parameter.Value != null)
                            {
                                if (jsonParams == "")
                                {
                                    jsonParams = "'"+ parameter.Value+"'";
                                }
                                else
                                {
                                    jsonParams = jsonParams + ",'" + parameter.Value + "'";
                                }
                            }
                        }
                        // Specify stored procedure name
                        command.CommandType = CommandType.Text;
                        command.CommandText = storedProcedure+"("+ jsonParams+")";
                        IDataReader reader = command.ExecuteReader();
                        DataTable dt = new DataTable();
                        var resultTable = reader.GetSchemaTable();
                        var columnCount = resultTable.Rows.Count;

                        for (int i = 0; i < columnCount; i++)
                        {
                            dt.Columns.Add(resultTable.Rows[i]["ColumnName"].ToString());
                        }

                        while (reader.Read())
                        {
                            DataRow dr = dt.NewRow();
                            for (int i = 0; i < columnCount; i++)
                            {
                                dr[i] = reader.GetString(i);
                            }
                            var val = reader.GetString(0);
                            dt.Rows.Add(dr);
                        }
                        ObtainedData.Tables.Add(dt);
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return ObtainedData;
        }

        public DataSet GetData(string storedProcedure)
        {
            DataSet ObtainedData = new DataSet();
            try
            {
                using (IDbConnection connection = CreateOpenConnection())
                {
                    using (IDbCommand command = CreateStoredProcCommand(storedProcedure, connection))
                    {
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
        public DataSet GetValueByQueryText(string queryText)
        {
            DataSet ObtainedData = new DataSet();

            try
            {
                using (IDbConnection connection = CreateOpenConnection())
                {
                    using (IDbCommand command = CreateCommand(queryText, connection))
                    {
                        IDataAdapter dataAdapter = CreateDataAdapter(command);
                        dataAdapter.Fill(ObtainedData);
                    }
                }
            }
            catch (Exception exe)
            {
                return null;
            }
            return ObtainedData;
        }
        public Object GetValue(string storedProcedure, out string generalMessage, out string technicalMessage)
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

        public Object GetValueByQueryText(string queryText, out string generalMessage, out string technicalMessage)
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

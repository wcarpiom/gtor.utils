using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Gtor.Utils.DbUtilities
{
    public interface IDbSysUtils
    {
        IEnumerable<SqlParameter> GetParametersFromSp(string connectionString, string storeProcedureName);
        void SetParametersToNull(SqlParameterCollection sqlParameters);
    }

    public class DbSysUtils : IDbSysUtils
    {
        private string _connectionString;
        private string _storeProcedureName;
        private string _tableName;

        public DbSysUtils() : this(null, null, null) { }

        public DbSysUtils(string connectionString, string storeProcedureName, string tableName)         {
            _connectionString = connectionString ?? ConfigurationManager.AppSettings["connectionString"];
            _storeProcedureName = storeProcedureName ?? ConfigurationManager.AppSettings["storedProcedure"];
            _tableName = tableName ?? ConfigurationManager.AppSettings["tableName"];
        }

        public IEnumerable<SqlParameter> GetParametersFromSp(string connectionString, string storeProcedureName)
        {
            var parameterList = new List<SqlParameter>();
            var sqlConnection = new SqlConnection(connectionString);
            var command = new SqlCommand(storeProcedureName, sqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };

            try
            {
                sqlConnection.Open();

                SqlCommandBuilder.DeriveParameters(command);

                foreach (SqlParameter sqlParameter in command.Parameters)
                {
                    if (sqlParameter.ParameterName == "@RETURN_VALUE")
                    {
                        continue;
                    }
                    parameterList.Add(new SqlParameter
                    {
                        ParameterName = sqlParameter.ParameterName,
                        DbType = sqlParameter.DbType
                    });
                }
                sqlConnection.Close();

            }
            catch (Exception e)
            {
                Debug.Write("It's not possible Get parameters from Stored Procedure: " + storeProcedureName + ", Error: " +
                            e.Message);
                throw;
            }

            return parameterList;
        }

        public void SetParametersToNull(SqlParameterCollection sqlParameters)
        {
            try
            {
                for (var parameterIndex = 0; parameterIndex < sqlParameters.Count; parameterIndex++)
                {
                    sqlParameters[parameterIndex].Value = DBNull.Value;
                }
            }
            catch (Exception e)
            {
                Debug.Write("It's not possible Set Parameters to Null, Error: " +
                            e.Message);
                throw;
            }
        }
    }
}
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
        IEnumerable<SqlParameter> GetParametersFromSP(string connectionString, string storedProcedureName);
        void SetParametersToNull(SqlParameterCollection sqlParameters);
        IEnumerable<string> GetTablesFromSP(string connectionString, string storedProcedureName);

    }

    public class DbSysUtils : IDbSysUtils
    {
        private string _connectionString;
        private string _storedProcedureName;

        public DbSysUtils() : this(null, null) { }

        public DbSysUtils(string connectionString, string storedProcedureName)
        {
            _connectionString = connectionString ?? ConfigurationManager.AppSettings["connectionString"];
            _storedProcedureName = storedProcedureName ?? ConfigurationManager.AppSettings["storedProcedure"];
        }

        public IEnumerable<SqlParameter> GetParametersFromSP(string connectionString, string storedProcedureName)
        {
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                _connectionString = connectionString;
            }
            if (!string.IsNullOrWhiteSpace(storedProcedureName))
            {
                _storedProcedureName = storedProcedureName;
            }

            var parameterList = new List<SqlParameter>();
            var sqlConnection = new SqlConnection(_connectionString);
            var command = new SqlCommand(_storedProcedureName, sqlConnection)
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
                Debug.Write("It's not possible Get parameters from Stored Procedure: " + _storedProcedureName + ", Error: " +
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
                Debug.Write("It's not possible Set Parameters to Null, Error: " + e.Message);
                throw;
            }
        }

        public IEnumerable<string> GetTablesFromSP(string connectionString, string storedProcedureName)
        {
            try
            {
                var sqlConnection = new SqlConnection(connectionString);
                var cmd = new SqlCommand
                {
                    CommandText = $"SELECT DISTINCT OBJECT_NAME(depid) AS Tables FROM sysdepends WHERE id = OBJECT_ID('{storedProcedureName}')",
                    CommandType = CommandType.Text,
                    Connection = sqlConnection
                };

                sqlConnection.Open();

                var reader = cmd.ExecuteReader();
                var dt = new DataTable();
                dt.Load(reader);

                var tablesList = new List<string>();
                for (var rowIndex = 0; rowIndex < dt.Rows.Count; rowIndex++)
                {
                    tablesList.Add(dt.Rows[rowIndex]["Tables"].ToString());
                }

                sqlConnection.Close();

                return tablesList;
            }
            catch (Exception exception)
            {
                Debug.Write($"It's not possible to return a tables from this Stored Procedure: {storedProcedureName}.\nError: {exception.Message}");
                throw;
            }
        }

    }
}
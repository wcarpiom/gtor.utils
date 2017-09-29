using Gtor.Utils.DbUtilities;
using NUnit.Framework;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Gtor.Utils.Test.DbUtilities
{
    [TestFixture(TestOf = typeof(DbSysUtils))]
    [System.ComponentModel.Category("Integration")]
    public class DbSysUtilsTests
    {
        private string _connectionString;
        private string _storedProcedureName;

        private DbSysUtils _target;

        [SetUp]
        public void SetUp()
        {
            _connectionString = "Data Source=ky1-vrt-msqld1.ky.cafepress.com;" +
                                "Initial Catalog=transit;User ID=cpdba;Password=ithinkgreen";

            _target = new DbSysUtils(_connectionString, _storedProcedureName);
        }

        [Test]
        public void Test_GetParametersFromSP()
        {
            //Arrange
            const string expected = "@inOrderNo";
            _storedProcedureName = "GET_SHIPMENT_BY_ORDER_NO";

            // Act
            var listParameters = _target.GetParametersFromSP(_connectionString, _storedProcedureName).ToList();

            // Assert
            Assert.AreEqual(expected, listParameters.FirstOrDefault()?.ParameterName);
        }

        [Test]
        public void Test_GetParametersFromSP_OnAnyError()
        {
            //Arrange
            _connectionString = "badConnectionString";
            _storedProcedureName = "badStoredProcedure";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _target.GetParametersFromSP(_connectionString, _storedProcedureName));
        }

        [Test]
        public void Test_SetParametersToNull()
        {
            //Arrange
            var command = new SqlCommand();
            command.Parameters.Add(new SqlParameter("ORDER_NO", DbType.Int64));
            command.Parameters[0].Value = int.MaxValue;

            var sqlParameters = command.Parameters;

            // Act
            _target.SetParametersToNull(sqlParameters);

            // Assert
            Assert.AreEqual(DBNull.Value, command.Parameters[0].Value);
        }

        [Test]
        public void Test_GetTablesFromSP()
        {
            // Arrange
            const string expected = "SHIPMENT";
            _storedProcedureName = "GET_SHIPMENT_BY_ORDER_NO";

            // Act
            var result = _target.GetTablesFromSP(_connectionString, _storedProcedureName);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.FirstOrDefault());
        }

        [Test]
        public void Test_GetTablesFromSP_OnAnyError()
        {
            // Arrange
            _connectionString = "testconnectionString";
            _storedProcedureName = "teststoreProcedure";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _target.GetTablesFromSP(_connectionString, _storedProcedureName));
        }
    }
}
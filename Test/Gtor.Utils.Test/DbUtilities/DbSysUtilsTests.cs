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
        private string _storeProcedureName;

        private DbSysUtils _target;

        [SetUp]
        public void SetUp()
        {
            _connectionString = "Data Source=ky1-vrt-msqld1.ky.cafepress.com;" +
                                "Initial Catalog=transit;User ID=cpdba;Password=ithinkgreen";

            _target = new DbSysUtils(_connectionString, _storeProcedureName);
        }

        [Test]
        public void Test_GetParametersFromSp()
        {
            //Arrange
            const string expected = "@inOrderNo";
            _storeProcedureName = "GET_SHIPMENT_BY_ORDER_NO";

            // Act
            var listParameters = _target.GetParametersFromSp(_connectionString, _storeProcedureName).ToList();

            // Assert
            Assert.AreEqual(expected, listParameters.FirstOrDefault()?.ParameterName);
        }

        [Test]
        public void Test_GetParametersFromSp_OnAnyError()
        {
            //Arrange
            _connectionString = "badConnectionString";
            _storeProcedureName = "badStoredProcedure";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _target.GetParametersFromSp(_connectionString, _storeProcedureName));
        }

        [Test]
        public void Test_SetParametersToNull()
        {
            //Arrange
            var command = new SqlCommand();
            command.Parameters.Add(new SqlParameter("Name", DbType.Int64));
            command.Parameters[0].Value = int.MaxValue;

            var sqlParameters = command.Parameters;

            // Act
            _target.SetParametersToNull(sqlParameters);

            // Assert
            Assert.AreEqual(DBNull.Value, command.Parameters[0].Value);
        }
    }
}
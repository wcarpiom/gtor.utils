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
        private string _tableName;

        private DbSysUtils _target;

        [SetUp]
        public void SetUp()
        {
            _connectionString = "Data Source=localhost;" +
                                "Initial Catalog=SoTour;" +
                                "Integrated Security=SSPI;";
            _storeProcedureName = "GET_PLAYER";
            _tableName = "Player";

            _target = new DbSysUtils(_connectionString, _storeProcedureName, _tableName);
        }

        [TearDown]
        public void TearDown()
        {
            _target = null;
            _connectionString = null;
            _storeProcedureName = null;
            _tableName = null;
        }


        [Test]
        public void Test_GetParametersFromSp()
        {
            //Arrange
            const string expected = "@inId";
            _storeProcedureName = "GET_PLAYER_BY_ID";

            // Act
            var listParameters = _target.GetParametersFromSp(_connectionString, _storeProcedureName).ToList();

            // Assert
            Assert.AreEqual(expected, listParameters[0].ParameterName);
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
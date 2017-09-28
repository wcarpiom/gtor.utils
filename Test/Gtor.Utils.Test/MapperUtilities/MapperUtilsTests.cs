using Gtor.Utils.MapperUtilities;
using NUnit.Framework;
using System;

namespace Gtor.Utils.Test.MapperUtilities
{
    [TestFixture(TestOf = typeof(MapperUtils)), Category("Unit")]

    public class MapperUtilsTests
    {
        private IMapperUtils _target;
        private Type _stringTypeTest;

        [SetUp]
        public void SetUp()
        {
            _target = new MapperUtils();
            _stringTypeTest = typeof(int);
        }

        [Test]
        public void Test_GetTypeByFriendlyName()
        {
            var expected = "int";

            var result = _target.GetStringTypeByType(_stringTypeTest);

            Assert.AreEqual(expected, result);
        }
    }
}
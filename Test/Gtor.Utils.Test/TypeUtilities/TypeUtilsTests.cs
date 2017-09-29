using Gtor.Utils.TypeUtilities;
using NUnit.Framework;
using System;

namespace Gtor.Utils.Test.TypeUtilities
{
    [TestFixture(TestOf = typeof(TypeUtils)), Category("Unit")]

    public class MapperUtilsTests
    {
        private ITypeUtils _target;
        private Type _stringTypeTest;

        [SetUp]
        public void SetUp()
        {
            _target = new TypeUtils();
            _stringTypeTest = typeof(int);
        }

        [Test]
        public void Test_GetTypeByFriendlyName()
        {
            const string expected = "int";

            var result = _target.GetFriendlyNameByType(_stringTypeTest);

            Assert.AreEqual(expected, result);
        }
    }
}
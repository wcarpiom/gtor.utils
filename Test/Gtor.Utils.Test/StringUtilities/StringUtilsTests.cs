using Gtor.Utils.Models;
using Gtor.Utils.StringUtilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Gtor.Utils.Test.StringUtilities
{
    [TestFixture(TestOf = typeof(StringUtils)), Category("Unit")]

    public class StringUtilsTests
    {
        private string _inputWord;

        [SetUp]
        public void SetUp()
        {
            _inputWord = "WoRd";
        }

        [TearDown]
        public void TearDown()
        {
            _inputWord = null;
        }

        [Test]
        public void Test_TransformTo_SentenceCase()
        {
            const string expected = "Word";

            var result = _inputWord.TransformTo(CaseType.SentenceCase);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Test_TransformTo_SentenceCaseWhenInputLengthIsOne()
        {
            // Arrange
            const string inputWord = "W";
            const string expected = "W";

            // Act
            var result = inputWord.TransformTo(CaseType.SentenceCase);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Test_TransformTo_UpperCase()
        {
            const string expected = "WORD";

            var result = _inputWord.TransformTo(CaseType.UpperCase);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Test_TransformTo_LowerCase()
        {
            const string expected = "word";

            var result = _inputWord.TransformTo(CaseType.LowerCase);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Test_TransformTo_FirstLetterInLower()
        {
            const string expected = "woRd";

            var result = _inputWord.TransformTo(CaseType.FirstLetterInLower);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Test_TransformTo_LowerCaseWhenInputLengthIsOne()
        {
            // Arrange
            const string inputWord = "W";
            const string expected = "w";

            // Act
            var result = inputWord.TransformTo(CaseType.LowerCase);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Test_TransformTo_WhenInputIsNullEmptyOrWhiteSpace()
        {
            const string inputWord = "";

            Assert.Throws<ArgumentException>(() => inputWord.TransformTo(CaseType.SentenceCase));
        }

        [Test]
        public void Test_TransformTo_ArgumentOutOfRangeException()
        {
            CaseType badCaseType = (CaseType)(-1);

            Assert.Throws<ArgumentOutOfRangeException>(() => _inputWord.TransformTo(badCaseType));
        }

        [Test]
        public void Test_SplitListOfWordsInUpperCase()
        {
            // Arrange
            var inputList = new List<string> { "133WoodlandDr", "350WestBroadway" };
            var expectedOutputList = new List<string> { "133 Woodland Dr", "350 West Broadway" };

            // Act
            var result = inputList.SplitListOfWordsInUpperCase();

            // Assert
            Assert.AreEqual(expectedOutputList, result);
        }
    }
}
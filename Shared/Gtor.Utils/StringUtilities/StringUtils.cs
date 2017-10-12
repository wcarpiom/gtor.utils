using Gtor.Utils.Models;
using System;
using System.Collections.Generic;

namespace Gtor.Utils.StringUtilities
{
    public static class StringUtils
    {
        public static string TransformTo(this string input, CaseType caseType)
        {
            string outputInCaseType;

            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Input string is null, empty or white space: ", input);

            switch (caseType)
            {
                case CaseType.SentenceCase:
                    outputInCaseType = char.ToUpper(input[0]) + input.Substring(1).ToLower();
                    break;
                case CaseType.UpperCase:
                    outputInCaseType = input.ToUpper();
                    break;
                case CaseType.LowerCase:
                    outputInCaseType = input.ToLower();
                    break;
                case CaseType.FirstLetterInLower:
                    outputInCaseType = char.ToLower(input[0]) + input.Substring(1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"That option is not available for {nameof(caseType)} , {caseType}");
            }
            return outputInCaseType;
        }

        private static string SplitWordInUpperCase(this string input)
        {
            string output = null;

            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Input string is null, empty or white space: ", input);

            foreach (var letter in input)
            {
                if (char.IsUpper(letter))
                    output += " " + letter;
                else
                    output += letter;
            }
            return output;
        }

        public static List<string> SplitListOfWordsInUpperCase(this List<string> inputList)
        {
            if (inputList == null)
                throw new ArgumentException("List input string is null, empty or white space");

            var outputList = new List<string>();
            foreach (var input in inputList)
            {
                outputList.Add(input.SplitWordInUpperCase());
            }
            return outputList;
        }
    }
}

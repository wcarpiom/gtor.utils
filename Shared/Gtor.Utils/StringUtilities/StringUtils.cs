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

        private static string EatWholeNumber(string input, ref int index)
        {
            var result = string.Empty;
            while (index < input.Length && char.IsNumber(input[index]))
            {
                result += input[index];
                index++;
            }
            index--;
            return result;
        }

        private static string SpecialAddressSplit(this string input)
        {
            var output = string.Empty;
            input = input ?? string.Empty;

            for (var index = 0; index < input.Length; index++)
            {
                if (char.IsNumber(input[index]) && index == 0)
                    output += EatWholeNumber(input, ref index);
                else if (char.IsNumber(input[index]) && index > 0)
                    output += " " + EatWholeNumber(input, ref index);
                else if (char.IsUpper(input[index]))
                    output += " " + input[index];
                else if (char.IsLower(input[index]))
                    output += input[index];
                else
                    output += input[index];
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
                outputList.Add(input.SpecialAddressSplit());
            }
            return outputList;
        }
    }
}

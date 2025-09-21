using System.Text;
using System.Text.RegularExpressions;

namespace TryliomUtility
{
    public abstract class StringUtility
    {
        public static string FormatEnumName(string enumName)
        {
            return Regex.Replace(enumName, "(?<!^)([A-Z])", " $1");
        }
        
        public static string ToLocalizeKey(string enumName)
        {
            return enumName.Replace(" ", "_").ToUpper();
        }
        
        public static string EnumToLocalizeKey(string enumName)
        {
            return ToLocalizeKey(FormatEnumName(enumName));
        }
        
        public static string ExtractNumber(ref int index, string description)
        {
            var number = "";
            
            while (index < description.Length && (char.IsDigit(description[index]) || description[index] == '.'))
            {
                number += description[index];
                index++;
            }
            
            return number;
        }
        
        public static string NicifyVariableName(string input)
        {
            var result = new StringBuilder(input.Length * 2);

            var prevIsLetter = false;
            var prevIsLetterUpper = false;
            var prevIsDigit = false;
            var prevIsStartOfWord = false;
            var prevIsNumberWord = false;

            var firstCharIndex = 0;
            if (input.StartsWith('_'))
            {
                firstCharIndex = 1;
            }
            else if (input.StartsWith("m_"))
            {
                firstCharIndex = 2;
            }

            for (var i = input.Length - 1; i >= firstCharIndex; i--)
            {
                var currentChar = input[i];
                var currIsLetter = char.IsLetter(currentChar);
                
                if (i == firstCharIndex && currIsLetter)
                {
                    currentChar = char.ToUpper(currentChar);
                }
                
                var currIsLetterUpper = char.IsUpper(currentChar);
                var currIsDigit = char.IsDigit(currentChar);
                var currIsSpacer = currentChar is ' ' or '_';

                var addSpace = (currIsLetter && !currIsLetterUpper && prevIsLetterUpper) ||
                               (currIsLetter && prevIsLetterUpper && prevIsStartOfWord) ||
                               (currIsDigit && prevIsStartOfWord) ||
                               (!currIsDigit && prevIsNumberWord) ||
                               (currIsLetter && !currIsLetterUpper && prevIsDigit);

                if (!currIsSpacer && addSpace)
                {
                    result.Insert(0, ' ');
                }

                result.Insert(0, currentChar);
                prevIsStartOfWord = currIsLetter && currIsLetterUpper && prevIsLetter && !prevIsLetterUpper;
                prevIsNumberWord = currIsDigit && prevIsLetter && !prevIsLetterUpper;
                prevIsLetterUpper = currIsLetter && currIsLetterUpper;
                prevIsLetter = currIsLetter;
                prevIsDigit = currIsDigit;
            }

            return result.ToString();
        }
    }
    
    public static class StringExtensions
    {
        public static string ToFirstUpper(this string str)
        {
            return char.ToUpper(str[0]) + str[1..];
        }
    }
}

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
    }
    
    public static class StringExtensions
    {
        public static string ToFirstUpper(this string str)
        {
            return char.ToUpper(str[0]) + str[1..];
        }
    }
}

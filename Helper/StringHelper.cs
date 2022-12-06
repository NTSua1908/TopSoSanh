using System.Text.RegularExpressions;

namespace TopSoSanh.Helper
{
    public static class StringHelper
    {
        public static string RemoveSpaces(this string value)
        {
            return Regex.Replace(value, @"\s+", " ");
        }

        public static string GetNumbers(this string input)
        {
            //return input;
            //Console.WriteLine(input);
            //int indexFirstSpecialCharacter = input.RegexIndexOf(@"?!\d");
            //if (indexFirstSpecialCharacter != -1)
            //    input = input.Substring(0, indexFirstSpecialCharacter - 1);
            bool isDone = false;
            input = input.Trim();
            return new string(input.Where(c =>
            {
                if (isDone)
                    return false;
                if (c != ',' && c != '.' && !char.IsDigit(c))
                {
                    isDone = true;
                    return false;
                }
                if (char.IsDigit(c))
                    return true;
                return false;
            }).ToArray());
        }

        public static int RegexIndexOf(this string str, string pattern)
        {
            var m = Regex.Match(str, pattern);
            return m.Success ? m.Index : -1;
        }

        public static string RemoveBreakLineTab(this string input)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input)) return "";

            var line = input
                .Replace("\t", string.Empty)
                .Replace("\r", string.Empty)
                .Replace("\n", string.Empty)
                .RemoveSpaces()
                .Trim();

            return line;
        }
    }
}

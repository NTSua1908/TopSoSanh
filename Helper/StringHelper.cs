using System.Text.RegularExpressions;

namespace TopSoSanh.Helper
{
    public static class StringHelper
    {
        private static string RemoveSpaces(this string value)
        {
            return Regex.Replace(value, @"\s+", " ");
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

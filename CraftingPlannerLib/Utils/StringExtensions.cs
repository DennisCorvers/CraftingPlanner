using System.Globalization;
using System.Text;

namespace CraftingPlannerLib.Utils
{
    internal static class StringExtensions
    {
        private static readonly CultureInfo EnCultureInfo = new CultureInfo("en-US");

        public static string ToFirstLetterUpperCase(this string value)
            => EnCultureInfo.TextInfo.ToTitleCase(value);

        public static string ToString(this double value, byte precision)
        {
            var rounded = Math.Round(value, precision);
            var format = string.Empty;

            if (precision == 0)
                format = "{0:#,##0}";
            else
            {
                var sb = new StringBuilder(10 + precision);
                sb.Append("{0:#,##0.");
                for (int i = 0; i < precision; i++)
                    sb.Append('#');
                sb.Append('}');

                format = sb.ToString();
            }

            return string.Format(format, rounded);
        }
    }
}

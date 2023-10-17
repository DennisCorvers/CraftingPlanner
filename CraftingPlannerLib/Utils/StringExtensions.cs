using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CraftingPlannerLib.Utils
{
    internal static class StringExtensions
    {
        private static readonly CultureInfo EnCultureInfo = new CultureInfo("en-US");

        public static bool TryParseFraction(this string fraction, out double result)
        {
            if (double.TryParse(fraction, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out result))
                return true;

            string[] split = fraction.Split(new char[] { ' ', '/' });

            if (split.Length == 2 || split.Length == 3)
            {

                if (int.TryParse(split[0], out int a) && int.TryParse(split[1], out int b))
                {
                    if (split.Length == 2)
                    {
                        result = (double)a / b;
                        return true;
                    }


                    if (int.TryParse(split[2], out int c))
                    {
                        result = a + (double)b / c;
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsInteger(this string value)
        {
            return int.TryParse(value, out _);
        }

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

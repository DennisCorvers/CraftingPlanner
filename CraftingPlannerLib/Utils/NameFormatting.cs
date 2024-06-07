using System.Globalization;

namespace CraftingPlannerLib.Utils
{
    internal static class NameFormatting
    {
        private static TextInfo TextInfo { get; } = new CultureInfo("en-US", false).TextInfo;

        public static string FormatName(string name) => TextInfo.ToTitleCase(name.Replace("_", " "));
    }
}

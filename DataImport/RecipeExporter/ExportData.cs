using Newtonsoft.Json;

namespace DataImport.RecipeExporter
{
    internal class ExportData
    {
        public IReadOnlyList<Recipe> Recipes { get; }

        public IReadOnlyDictionary<int, string> ModLookup { get; }

        public IReadOnlyDictionary<int, Item> ItemLookup { get; }

        [JsonConstructor]
        public ExportData(
            [JsonProperty("Recipes")] List<Recipe> recipes,
            [JsonProperty("Mods")] Dictionary<int, string> mods,
            [JsonProperty("Items")] Dictionary<int, Item> items)
        {
            Recipes = recipes;
            ModLookup = mods;
            ItemLookup = items;
        }
    }
}

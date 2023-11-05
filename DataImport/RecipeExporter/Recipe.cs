using Newtonsoft.Json;

namespace DataImport.RecipeExporter
{
    [Serializable]
    internal class Recipe
    {
        public IReadOnlyList<ItemStack> Input { get; }

        public ItemStack Output { get; }

        [JsonConstructor]
        public Recipe(
            [JsonProperty("In")] List<ItemStack> input, 
            [JsonProperty("Out")] ItemStack output)
        {
            Input = input;
            Output = output;
        }
    }
}

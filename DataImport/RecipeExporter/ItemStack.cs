using DataImport.RecipeExporter.JsonConverters;
using Newtonsoft.Json;

namespace DataImport.RecipeExporter
{
    internal class ItemStack
    {
        public int Amount { get; }

        public int ItemID { get; }

        public IReadOnlySet<int>? AlternativeItemIds { get; }

        [JsonConstructor]
        public ItemStack(
            [JsonProperty("Amount")] int amount,
            [JsonProperty("ItemID")] int itemID,
            [JsonProperty("AlternativeInputs")] List<int> alternativeItemIds)
        {
            Amount = amount;
            ItemID = itemID;

            if (alternativeItemIds != null)
                AlternativeItemIds = new HashSet<int>(alternativeItemIds);
        }
    }
}

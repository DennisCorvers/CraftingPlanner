using DataImport.RecipeExporter.JsonConverters;
using Newtonsoft.Json;

namespace DataImport.RecipeExporter
{
    [JsonConverter(typeof(ItemStackJsonConverter))]
    internal class ItemStack
    {
        public int Amount { get; }

        public int ItemID { get; }

        [JsonConstructor]
        public ItemStack(
            [JsonProperty("Amount")] int amount,
            [JsonProperty("ItemID")] int itemID)
        {
            Amount = amount;
            ItemID = itemID;
        }
    }

    internal class AlternativeItemStack : ItemStack
    {
        public IReadOnlyList<int> AlternativeItemIds { get; }

        [JsonConstructor]
        public AlternativeItemStack(
            [JsonProperty("Amount")] int amount,
            [JsonProperty("ItemID")] int itemID,
            [JsonProperty("AlternativeInputs")] List<int> alternativeItemIds)
            : base(amount, itemID)
        {
            AlternativeItemIds = alternativeItemIds;
        }
    }
}

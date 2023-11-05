using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DataImport.RecipeExporter.JsonConverters
{
    internal class ItemStackJsonConverter : JsonConverter<ItemStack>
    {
        public override ItemStack? ReadJson(JsonReader reader, Type objectType, ItemStack? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);
            var amount = jObject.Value<int>("Amount");
            var itemID = jObject.Value<int>("ItemID");

            if (jObject.TryGetValue("AlternativeInputs", out JToken? value))
            {
                var alternativeIds = value.ToObject<List<int>>();

                if (alternativeIds != null && alternativeIds.Count > 0)
                    return new AlternativeItemStack(amount, itemID, alternativeIds);
            }

            return new ItemStack(amount, itemID);
        }

        public override void WriteJson(JsonWriter writer, ItemStack? value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}

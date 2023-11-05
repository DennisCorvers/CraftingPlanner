using Newtonsoft.Json;
using System.Diagnostics;

namespace DataImport.RecipeExporter
{
    [DebuggerDisplay("{Name}")]
    internal class Item
    {
        public string Name { get; }

        public int ModID { get; }

        [JsonConstructor]
        public Item(
            [JsonProperty("Name")] string name, 
            [JsonProperty("ModID")] int modID)
        {
            Name = name;
            ModID = modID;
        }
    }
}

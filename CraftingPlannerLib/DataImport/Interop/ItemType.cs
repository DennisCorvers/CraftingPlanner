using System.Text.Json.Serialization;

namespace CraftingPlannerLib.DataImport.External
{
    [Serializable]
    internal class ItemType
    {
        [JsonConstructor]
        public ItemType(int iD, string name)
        {
            ID = iD;
            Name = name;
        }

        public int ID { get; }

        public string Name { get; }
    }
}

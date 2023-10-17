using System.Text.Json.Serialization;

namespace CraftingPlannerLib.DataImport
{
    [Serializable]
    internal class ItemTypeEntity
    {
        [JsonConstructor]
        public ItemTypeEntity(int iD, string name)
        {
            ID = iD;
            Name = name;
        }

        public int ID { get; }

        public string Name { get; }
    }
}

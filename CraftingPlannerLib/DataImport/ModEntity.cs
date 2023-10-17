using System.Text.Json.Serialization;

namespace CraftingPlannerLib.DataImport
{
    [Serializable]
    internal class ModEntity
    {
        [JsonConstructor]
        public ModEntity(int iD, string name)
        {
            ID = iD;
            Name = name;
        }

        public int ID { get; }

        public string Name { get; }
    }
}

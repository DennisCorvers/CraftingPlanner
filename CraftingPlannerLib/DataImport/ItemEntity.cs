using System.Text.Json.Serialization;

namespace CraftingPlannerLib.DataImport
{
    [Serializable]
    internal class ItemEntity
    {
        [JsonConstructor]
        public ItemEntity(int id, string name, int typeID, int modID, Dictionary<int, double> recipes)
        {
            ID = id;
            Name = name;
            TypeID = typeID;
            ModID = modID;
            Recipes = recipes;
        }

        public int ID { get; }

        public string Name { get; }

        public int TypeID { get; }

        public int ModID { get; }

        public Dictionary<int, double> Recipes { get; }
    }
}

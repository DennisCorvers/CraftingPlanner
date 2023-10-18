using System.Text.Json.Serialization;

namespace CraftingPlannerLib.DataImport.External
{
    [Serializable]
    internal class Item
    {
        [JsonConstructor]
        public Item(int id, string name, int typeID, int modID, Dictionary<int, double> recipes)
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

using System.Reflection.Metadata.Ecma335;

namespace CraftingPlannerLib.Entities
{
    public class ItemType : Entity
    {
        public string Name { get; set; }

        public ItemType(string name)
        {
            Name = name;
        }
    }
}

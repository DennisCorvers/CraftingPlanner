using System.Diagnostics;

namespace CraftingPlannerLib.Models
{
    [DebuggerDisplay("{Name}")]
    public abstract class BaseModel
    {
        public int ID { get; }

        public string Name { get; }

        public BaseModel(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}

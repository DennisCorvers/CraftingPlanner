using CraftingPlannerLib.Utils;

namespace CraftingPlannerLib.Models
{
    public sealed class Mod : BaseModel
    {
        public IReadOnlyList<Item> Items { get; private set; }

        public Mod(int id, string name, IReadOnlyList<Item> items)
            : base(id, NameFormatting.FormatName(name))
        {
            Items = items;
        }

        public void SetItems(IEnumerable<Item> items)
        {
            Items = items.OrderBy(x => x.Name).ToList();
        }
    }
}

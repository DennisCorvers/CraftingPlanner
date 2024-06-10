using CraftingPlannerLib.Models;

namespace CraftingPlannerLib.Data.GameData
{
    public interface IItemRepository : IReadonlyRepository<Item>
    {
        IEnumerable<Item> Find(string name);
        IEnumerable<Item> NameContains(string name);
    }
}

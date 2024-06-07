using CraftingPlannerLib.Models;

namespace CraftingPlannerLib.Data
{
    public interface IItemRepository : IReadonlyRepository<Item>
    {
        IEnumerable<Item> Find(string name);
        IEnumerable<Item> NameContains(string name);
    }
}

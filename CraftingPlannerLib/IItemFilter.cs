using CraftingPlannerLib.Entities;

namespace CraftingPlannerLib
{
    public interface IItemFilter
    {
        IEnumerable<Item> Filter(IEnumerable<Item> items);
    }
}

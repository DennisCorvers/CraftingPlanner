using CraftingPlannerLib;
using CraftingPlannerLib.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CraftingPlanner.ViewModels.Filters
{
    internal class ItemNameFilter : IItemFilter
    {
        public string? ItemName;

        public IEnumerable<Item> Filter(IEnumerable<Item> items)
        {
            return ItemName == null ?
                items :
                items.Where(x => x.Name.Contains(ItemName, System.StringComparison.OrdinalIgnoreCase));
        }
    }
}

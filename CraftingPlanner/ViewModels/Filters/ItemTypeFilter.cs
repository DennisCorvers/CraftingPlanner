using CraftingPlannerLib;
using CraftingPlannerLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CraftingPlanner.ViewModels.Filters
{
    internal class ItemTypeFilter : IItemFilter
    {
        public ItemType? ItemType;

        public IEnumerable<Item> Filter(IEnumerable<Item> items)
        {
            return ItemType == null ?
                items :
                items.Where(x => x.Type == ItemType);
        }
    }
}

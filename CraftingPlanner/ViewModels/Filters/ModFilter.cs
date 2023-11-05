using CraftingPlannerLib;
using CraftingPlannerLib.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CraftingPlanner.ViewModels.Filters
{
    internal class ModFilter : IItemFilter
    {
        public Mod? Mod;

        public IEnumerable<Item> Filter(IEnumerable<Item> items)
        {
            return Mod == null ?
                items :
                items.Where(x => x.Mod == Mod);
        }
    }
}

using CraftingPlanner.Models;
using CraftingPlannerLib;
using CraftingPlannerLib.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CraftingPlanner.ViewModels.Filters
{
    internal class OutputTypeFilter : IItemFilter
    {
        public OutputType Type;

        public IEnumerable<Item> Filter(IEnumerable<Item> items)
        {
            if (Type == OutputType.Ingredient)
                return items.Where(x => x.Recipe != null && x.Recipe.ContainsKey(x));

            return items;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CraftingPlannerLib.DAL.Entities;

namespace CraftingPlannerLib.Calculation
{
    public interface IRecipeCalculator
    {
        Dictionary<Item, double> CalculateRecipe(Item baseItem, double requiredAmount = 1, bool returnbase = false);
    }
}

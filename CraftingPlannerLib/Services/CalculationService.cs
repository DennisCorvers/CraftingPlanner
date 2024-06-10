using CraftingPlannerLib.Calculation;
using CraftingPlannerLib.Data;
using CraftingPlannerLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftingPlannerLib.Services
{
    public class CalculationService
    {
        private IRecipeRepository m_recipeRepository;

        public CalculationService(IRecipeRepository recipeRepository)
        {
            m_recipeRepository = recipeRepository;
        }

        public CalculationResult Calculate(Item target, int amount)
        {
            var calculator = new Calculator(this, target);
            return calculator.Calculate(amount);
        }

        public Recipe? GetItemRecipe(Item target)
        {
            var recipeGrouping = m_recipeRepository.FindAsOutput(target);

            // Return favourite recipe instead of just the first.
            return recipeGrouping.Recipes.FirstOrDefault();
        }
    }
}

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

        public void Calculate(Item target, int amount)
        {
            var calculator = new Calculation.Calculator(this, target);

        }

        public void GetItemRecipes(Item target)
        {;
            var items = m_recipeRepository.FindAsOutput(target);
        }
    }
}

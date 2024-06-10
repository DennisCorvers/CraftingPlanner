using CraftingPlannerLib.Calculation;
using CraftingPlannerLib.Data.GameData;
using CraftingPlannerLib.Data.UserData;
using CraftingPlannerLib.Models;

namespace CraftingPlannerLib.Services
{
    public class CalculationService
    {
        private readonly IRecipeRepository m_recipeRepository;
        private readonly ICustomRecipeRepository m_userDataRepository;

        public CalculationService(ICustomRecipeRepository userDataRepository, IRecipeRepository recipeRepository)
        {
            m_recipeRepository = recipeRepository;
            m_userDataRepository = userDataRepository;
        }

        public CalculationResult Calculate(Item target, int amount)
        {
            var calculator = new Calculator(this, target);
            return calculator.Calculate(amount);
        }

        public Recipe? GetItemRecipe(Item target)
        {
            // A specific item can be the result of multiple Recipes.
            // We try to find the user-favourited recipe, or else the first option.
            var recipeGrouping = m_recipeRepository.FindAsOutput(target);
            if (recipeGrouping.RecipeCount > 1)
            {
                return m_userDataRepository.GetFavouriteRecipe(target);
            }
            else
            {
                return recipeGrouping.Recipes.FirstOrDefault();
            }
        }
    }
}

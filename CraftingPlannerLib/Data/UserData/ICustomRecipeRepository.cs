using CraftingPlannerLib.Data.GameData;
using CraftingPlannerLib.Models;

namespace CraftingPlannerLib.Data.UserData
{
    public interface ICustomRecipeRepository : IRecipeRepository
    {
        Recipe? GetFavouriteRecipe(Item item);

        void AddRecipe(Recipe recipe);

        void UpdateRecipe(int id, Recipe newData);

        void DeleteRecipe(int id);
    }
}

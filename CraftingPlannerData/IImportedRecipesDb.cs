using CraftingPlannerLib.RecipeDB.Services;

namespace CraftingPlannerData
{
    public interface IImportedRecipesDb
    {
        ItemService ItemService { get; }
        ModService ModService { get; }
        RecipeService RecipeService { get; }
    }
}

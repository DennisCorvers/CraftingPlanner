using CraftingPlannerLib.RecipeDB.Services;

namespace CraftingPlannerLib.RecipeDB
{
    public interface IImportedRecipesDb
    {
        ItemService ItemService { get; }
        ModService ModService { get; }
        RecipeService RecipeService { get; }
    }
}

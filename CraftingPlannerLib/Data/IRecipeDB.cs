using CraftingPlannerLib.Services;

namespace CraftingPlannerData
{
    public interface IRecipeDB
    {
        ItemService ItemService { get; }
        ModService ModService { get; }
        RecipeService RecipeService { get; }
    }
}

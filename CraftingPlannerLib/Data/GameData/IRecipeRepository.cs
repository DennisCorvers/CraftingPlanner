using CraftingPlannerLib.Models;
using CraftingPlannerLib.RecipeDB.Models;

namespace CraftingPlannerLib.Data.GameData
{
    public interface IRecipeRepository : IReadonlyRepository<Recipe>
    {
        IEnumerable<RecipeGrouping> GetAll();

        IEnumerable<RecipeGrouping> FindAsInput(IEnumerable<Item> inputItems);

        IEnumerable<RecipeGrouping> FindAsOutput(IEnumerable<Item> outputItems);

        IEnumerable<RecipeGrouping> FindRelated(IEnumerable<Item> relatedItems);

        RecipeGrouping FindAsOutput(Item outputItem);
    }
}

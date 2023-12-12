using CraftingPlannerLib.DAL;
using CraftingPlannerLib.RecipeDB.Models;
using DataImport.Models;

namespace CraftingPlannerLib.RecipeDB
{
    public interface IRecipeRepository : IReadonlyRepository<Recipe>
    {
        IEnumerable<RecipeGrouping> GetAll();

        IEnumerable<RecipeGrouping> FindAsInput(IEnumerable<Item> inputItems);

        IEnumerable<RecipeGrouping> FindAsOutput(IEnumerable<Item> outputItems);

        IEnumerable<RecipeGrouping> FindRelated(IEnumerable<Item> relatedItems);
    }
}

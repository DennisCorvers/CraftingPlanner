using CraftingPlannerLib.DAL;
using DataImport.Models;

namespace CraftingPlannerLib.RecipeDB
{
    public interface IRecipeRepository : IReadonlyRepository<Recipe>
    {
        IEnumerable<Recipe> FindAsInput(Item inputItem);

        IEnumerable<Recipe> FindAsOutput(Item outputItem);

        IEnumerable<Recipe> FindRelated(Item relatedItem);
    }
}

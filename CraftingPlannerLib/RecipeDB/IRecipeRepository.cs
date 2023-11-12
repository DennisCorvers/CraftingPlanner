using CraftingPlannerLib.DAL;
using DataImport.Models;

namespace CraftingPlannerLib.RecipeDB
{
    public interface IRecipeRepository : IReadonlyRepository<Recipe>
    {
        IEnumerable<Recipe> Find(string itemName, string modName);
    }
}

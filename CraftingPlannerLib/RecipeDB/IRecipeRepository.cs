using CraftingPlannerLib.DAL;
using DataImport.Models;

namespace CraftingPlannerLib.RecipeDB
{
    public interface IRecipeRepository : IReadonlyRepository<Recipe>
    {
        IEnumerable<Recipe> FindAsInput(Item inputItem);
        IEnumerable<Recipe> FindAsInput(IEnumerable<Item> inputItems);

        IEnumerable<Recipe> FindAsOutput(Item outputItem);
        IEnumerable<Recipe> FindAsOutput(IEnumerable<Item> outputItems);

        IEnumerable<Recipe> FindRelated(Item relatedItem);
        IEnumerable<Recipe> FindRelated(IEnumerable<Item> relatedItems);
    }
}

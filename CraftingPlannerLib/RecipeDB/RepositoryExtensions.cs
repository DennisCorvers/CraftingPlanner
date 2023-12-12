using CraftingPlannerLib.RecipeDB.EqualityComparers;
using CraftingPlannerLib.RecipeDB.Models;
using DataImport.Models;

namespace CraftingPlannerLib.RecipeDB
{
    public static class RepositoryExtensions
    {
        /// <summary>
        /// Groups distinct output-recipes by output item
        /// </summary>
        /// <param name="recipes">Collection of recipes to be grouped</param>
        public static IEnumerable<RecipeGrouping> GroupRecipes(this IEnumerable<Recipe> recipes)
        {
            return recipes
                .SelectMany(x => x.Output, (recipe, itemStack) =>
                {
                    return new RecipeIndexItem(itemStack.Item, recipe);
                })
                .DistinctBy(x => x)
                .GroupBy(
                    key => key.Item,
                    element => element.Recipe,
                    (item, recipes) =>
                    {
                        return new RecipeGrouping(item, recipes);
                    },
                    ItemIDEqualityComparer.Default);
        }

        private record struct RecipeIndexItem(Item Item, Recipe Recipe);
    }
}

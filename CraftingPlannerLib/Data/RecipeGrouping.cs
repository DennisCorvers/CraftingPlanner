

using CraftingPlannerLib.Models;

namespace CraftingPlannerLib.RecipeDB.Models
{
    public readonly struct RecipeGrouping
    {
        public Item Item { get; }

        public IEnumerable<Recipe> Recipes { get; }

        public int RecipeCount { get; }

        public RecipeGrouping(Item item, IEnumerable<Recipe> recipes)
        {
            Item = item;
            Recipes = recipes;
            RecipeCount = recipes.Count();
        }
    }
}

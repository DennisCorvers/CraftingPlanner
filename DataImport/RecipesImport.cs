using CraftingPlannerLib.Models;

namespace DataImport
{
    /// <summary>
    /// Readonly data that represents a recipe data import.
    /// </summary>
    public sealed class RecipesImport
    {
        public IReadOnlyList<Recipe> Recipes { get; }

        public IReadOnlyList<Mod> Mods { get; }

        public IReadOnlyList<Item> Items { get; }

        public RecipesImport(IReadOnlyList<Recipe> recipes, IEnumerable<Mod> mods, IEnumerable<Item> items)
        {
            Recipes = recipes;
            Mods = new List<Mod>(mods);
            Items = new List<Item>(items);
        }
    }
}

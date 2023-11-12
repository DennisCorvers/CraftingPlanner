using DataImport.EqualityComparers;
using DataImport.Models;
namespace DataImport
{
    public sealed class RecipeDB
    {
        public IReadOnlyList<Recipe> Recipes { get; }

        public IReadOnlyList<Mod> Mods { get; }

        public IReadOnlyList<Item> Items { get; }

        public RecipeDB(IReadOnlyList<Recipe> recipes, IEnumerable<Mod> mods, IEnumerable<Item> items)
        {
            Recipes = recipes;
            Mods = new List<Mod>(mods);
            Items = new List<Item>(items);
        }
    }
}

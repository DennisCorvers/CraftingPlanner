using DataImport.EqualityComparers;
using DataImport.Models;
namespace DataImport
{
    public sealed class RecipeDB
    {
        public IReadOnlyList<Recipe> Recipes { get; }

        public IReadOnlySet<Mod> Mods { get; }

        public IReadOnlySet<Item> Items { get; }

        public RecipeDB(IReadOnlyList<Recipe> recipes, IEnumerable<Mod> mods, IEnumerable<Item> items)
        {
            Recipes = recipes;
            Mods = new HashSet<Mod>(mods, BaseModelComparer.Default);
            Items = new HashSet<Item>(items, BaseModelComparer.Default);
        }
    }
}

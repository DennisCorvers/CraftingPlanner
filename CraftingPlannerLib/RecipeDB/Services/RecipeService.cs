using DataImport.EqualityComparers;
using DataImport.Models;
using System.Diagnostics.CodeAnalysis;

namespace CraftingPlannerLib.RecipeDB.Services
{
    public class RecipeService
    {
        private readonly IModRepository m_modRepository;
        private readonly IItemRepository m_itemRepository;
        private readonly IRecipeRepository m_recipeRepository;

        internal RecipeService(IModRepository modRepository, IItemRepository itemRepository, IRecipeRepository recipeRepository)
        {
            m_modRepository = modRepository;
            m_itemRepository = itemRepository;
            m_recipeRepository = recipeRepository;
        }

        public IEnumerable<Recipe> GetAll()
            => m_recipeRepository.Entities;

        public IEnumerable<Recipe> MatchRecipes(string itemName, string modName, ItemStackType itemStackType)
        {
            var mod = m_modRepository.Find(modName);

            if (mod == null)
                return Enumerable.Empty<Recipe>();

            var items = m_itemRepository
                .Find(itemName)
                .Where(x => x.Mod.ID == mod.ID);

            return FindAll(items, itemStackType);
        }

        public IEnumerable<Recipe> FindAll(string partialItemName, ItemStackType itemStackType)
        {
            var items = m_itemRepository.NameContains(partialItemName);
            return FindAll(items, itemStackType);
        }

        public IEnumerable<Recipe> FindAll(IEnumerable<Item> items, ItemStackType itemStackType)
        {
            var recipes = itemStackType switch
            {
                ItemStackType.Input => items.SelectMany(x => m_recipeRepository.FindAsInput(x)),
                ItemStackType.Output => items.SelectMany(x => m_recipeRepository.FindAsOutput(x)),
                ItemStackType.Any => items.SelectMany(x => m_recipeRepository.FindRelated(x)),
                _ => throw new InvalidOperationException()
            };

            return recipes.Distinct(RecipeEqualityComparer.Default);
        }

        private class RecipeEqualityComparer : IEqualityComparer<Recipe>
        {
            public static IEqualityComparer<Recipe> Default { get; } = new RecipeEqualityComparer();

            public bool Equals(Recipe? x, Recipe? y)
            {
                if (ReferenceEquals(x, y))
                    return true;

                if (y is null || x is null)
                    return false;

                return x.Output.Equals(y.Output)
                    && x.Input.SequenceEqual(y.Input);
            }

            public int GetHashCode([DisallowNull] Recipe obj)
            {
                return HashCode.Combine(obj.Output, ListHash(obj.Input));
            }

            private static int ListHash(IEnumerable<ItemStack> items)
            {
                unchecked
                {
                    int hash = 19;
                    foreach (var item in items)
                    {
                        hash = hash * 31 + item.GetHashCode();
                    }
                    return hash;
                }
            }
        }
    }
}

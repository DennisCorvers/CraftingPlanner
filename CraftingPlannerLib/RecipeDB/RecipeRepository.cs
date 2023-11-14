using DataImport.EqualityComparers;
using DataImport.Models;
using System.Diagnostics.CodeAnalysis;

namespace CraftingPlannerLib.RecipeDB
{
    internal class RecipeRepository : IRecipeRepository
    {
        private readonly IReadOnlyList<Recipe> m_recipes;
        private readonly ILookup<Item, Recipe> m_inputIndex;
        private readonly ILookup<Item, Recipe> m_outputIndex;

        public IEnumerable<Recipe> Entities
            => m_recipes;

        public RecipeRepository(IEnumerable<Recipe> recipes)
        {
            m_recipes = recipes
                .OrderBy(x => x.Output.Item.Name)
                .ToList();

            m_inputIndex = CreateInputIndex(recipes)
                .ToLookup(k => k.item, v => v.parent, ItemEqualityComparer.Default);

            m_outputIndex = CreateOutputIndex(recipes)
                .ToLookup(k => k.item, v => v.parent, ItemEqualityComparer.Default);
        }

        public IEnumerable<Recipe> FindAsOutput(Item outputItem)
        {
            return m_outputIndex[outputItem];
        }

        public IEnumerable<Recipe> FindAsInput(Item inputItem)
        {
            return m_inputIndex[inputItem];
        }

        public IEnumerable<Recipe> FindRelated(Item relatedItem)
        {
            return m_inputIndex[relatedItem]
                .Concat(m_outputIndex[relatedItem]);
        }

        private static IEnumerable<RecipeIndexItem> CreateInputIndex(IEnumerable<Recipe> recipes)
        {
            // Select all alternative items per input, then flatten to one IEnumerable.
            return recipes
                .SelectMany(x => x.Input, (recipe, itemstack) => { return ExpandInput(recipe, itemstack); })
                .SelectMany(x => x);

            static IEnumerable<RecipeIndexItem> ExpandInput(Recipe parent, ItemStack input)
            {
                var indexInput = new[] { new RecipeIndexItem(input.Item, parent) };
                if (input is AlternativeItemStack aStack)
                {
                    indexInput.Concat(aStack.AlternativeItems.Select(x => new RecipeIndexItem(x, parent)));
                }

                return indexInput;
            }
        }

        private static IEnumerable<RecipeIndexItem> CreateOutputIndex(IEnumerable<Recipe> recipes)
        {
            return recipes.Select(x => new RecipeIndexItem(x.Output.Item, x));
        }

        private record struct RecipeIndexItem(Item item, Recipe parent);

        private class ItemEqualityComparer : IEqualityComparer<Item>
        {
            public static ItemEqualityComparer Default = new ItemEqualityComparer();

            public bool Equals(Item? x, Item? y)
            {
                if (ReferenceEquals(x, y))
                    return true;

                if (y is null || x is null)
                    return false;

                return x.ID == y.ID;
            }

            public int GetHashCode([DisallowNull] Item obj) => obj.ID;
        }
    }
}

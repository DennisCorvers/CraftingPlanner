using DataImport.Models;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

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
            => FormatRecipesLookup(m_outputIndex[outputItem]);

        public IEnumerable<Recipe> FindAsOutput(IEnumerable<Item> outputItems)
            => FormatRecipesLookup(outputItems.SelectMany(x => m_outputIndex[x]));

        public IEnumerable<Recipe> FindAsInput(Item inputItem)
            => FormatRecipesLookup(m_inputIndex[inputItem]);

        public IEnumerable<Recipe> FindAsInput(IEnumerable<Item> inputItems)
            => FormatRecipesLookup(inputItems.SelectMany(x => m_inputIndex[x]));

        public IEnumerable<Recipe> FindRelated(Item relatedItem)
        {
            return FormatRecipesLookup(
                    m_outputIndex[relatedItem]
                    .Concat(m_inputIndex[relatedItem]));
        }

        public IEnumerable<Recipe> FindRelated(IEnumerable<Item> relatedItems)
        {
            var allItems = relatedItems.SelectMany(x => m_inputIndex[x])
                .Concat(relatedItems.SelectMany(x => m_outputIndex[x]));

            return FormatRecipesLookup(allItems);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerable<Recipe> FormatRecipesLookup(IEnumerable<Recipe> rawValues)
        {
            return rawValues
            .DistinctBy(x => x.Id)
            .OrderBy(x => x.Output.Item.Name);
        }

        private static IEnumerable<RecipeIndexItem> CreateInputIndex(IEnumerable<Recipe> recipes)
        {
            // Select all alternative items per input, then flatten to one IEnumerable.
            return recipes
                .SelectMany(x => x.Input, (recipe, itemstack) => { return ExpandInput(recipe, itemstack); })
                .SelectMany(x => x);

            static IEnumerable<RecipeIndexItem> ExpandInput(Recipe parent, ItemStack input)
            {
                IEnumerable<RecipeIndexItem> indexInput = new[] { new RecipeIndexItem(input.Item, parent) };
                if (input is AlternativeItemStack aStack)
                {
                    indexInput = indexInput.Concat(aStack.AlternativeItems.Select(x => new RecipeIndexItem(x, parent)));
                }

                return indexInput;
            }
        }

        private static IEnumerable<RecipeIndexItem> CreateOutputIndex(IEnumerable<Recipe> recipes)
        {
            return recipes.Select(x => new RecipeIndexItem(x.Output.Item, x));
        }

        private record struct RecipeIndexItem(Item item, Recipe parent);

        /// <summary>
        /// Comparer used to create (Item -> Recipe) indexes
        /// </summary>
        private class ItemEqualityComparer : IEqualityComparer<Item>
        {
            public static ItemEqualityComparer Default { get; } = new ItemEqualityComparer();

            public bool Equals(Item? x, Item? y)
            {
                // This can't ever happen!
                // Recipe Input and Output items are not nullable.
                Debug.Assert(x != null && y != null);

                return x.ID == y.ID;
            }

            public int GetHashCode([DisallowNull] Item obj) => obj.ID;
        }
    }
}

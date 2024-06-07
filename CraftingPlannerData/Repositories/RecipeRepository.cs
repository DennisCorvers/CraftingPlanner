using CraftingPlannerData.EqualityComparers;
using CraftingPlannerLib.Data;
using CraftingPlannerLib.Models;
using CraftingPlannerLib.RecipeDB.Models;

namespace CraftingPlannerData.Repositories
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
                .ToList();

            m_inputIndex = CreateInputIndex(recipes)
                .ToLookup(k => k.item, v => v.parent, ItemIDEqualityComparer.Default);

            m_outputIndex = CreateOutputIndex(recipes)
                .ToLookup(k => k.item, v => v.parent, ItemIDEqualityComparer.Default);
        }

        public IEnumerable<RecipeGrouping> FindAsOutput(IEnumerable<Item> outputItems)
        {
            return outputItems
                .Where(x => m_outputIndex.Contains(x))
                .Select(x =>
                {
                    return new RecipeGrouping(x, m_outputIndex[x]);
                });
        }

        public IEnumerable<RecipeGrouping> FindAsInput(IEnumerable<Item> inputItems)
        {
            return inputItems
                .SelectMany(x => m_inputIndex[x])
                .GroupRecipes();
        }

        public IEnumerable<RecipeGrouping> FindRelated(IEnumerable<Item> relatedItems)
        {
            var allItems = relatedItems.SelectMany(x => m_inputIndex[x])
                .Concat(relatedItems.SelectMany(x => m_outputIndex[x]));

            return allItems.GroupRecipes();
        }

        public IEnumerable<RecipeGrouping> GetAll()
            => m_outputIndex.Select(x => new RecipeGrouping(x.Key, x));

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
            return recipes.SelectMany(x => x.Output, (recipe, itemstack) => { return new RecipeIndexItem(itemstack.Item, recipe); });
        }

        private record struct RecipeIndexItem(Item item, Recipe parent);
    }
}

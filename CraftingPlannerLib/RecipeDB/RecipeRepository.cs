using DataImport.Models;

namespace CraftingPlannerLib.RecipeDB
{
    internal class RecipeRepository : IRecipeRepository
    {
        private readonly IReadOnlyList<Recipe> m_recipes;
        private readonly ILookup<string, Recipe> m_itemRecipeLookup;

        public IEnumerable<Recipe> Entities
            => m_recipes;

        public RecipeRepository(IEnumerable<Recipe> recipes)
        {
            m_recipes = recipes
                .OrderBy(x => x.Output.Item.Name)
                .ToList();

            m_itemRecipeLookup = recipes
                .ToLookup(x => x.Output.Item.Name);
        }

        public IEnumerable<Recipe> Find(string itemName, string modName)
        {
            return m_itemRecipeLookup[itemName]
                .Where(x=> StringComparer.OrdinalIgnoreCase.Equals(x.Output.Item.Mod.Name, modName));
        }
    }
}

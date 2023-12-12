using CraftingPlannerLib.RecipeDB.Models;
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

        public IEnumerable<RecipeGrouping> GetAll()
            => m_recipeRepository.GetAll();

        public IEnumerable<RecipeGrouping> MatchRecipes(string itemName, string modName, ItemStackType itemStackType)
        {
            var mod = m_modRepository.Find(modName);

            if (mod == null)
                return Enumerable.Empty<RecipeGrouping>();

            var items = m_itemRepository
                .Find(itemName)
                .Where(x => x.Mod.ID == mod.ID);

            return FindAll(items, itemStackType);
        }

        public IEnumerable<RecipeGrouping> FindAll(string partialItemName, ItemStackType itemStackType)
        {
            var items = m_itemRepository.NameContains(partialItemName);
            return FindAll(items, itemStackType);
        }

        public IEnumerable<RecipeGrouping> FindAll(IEnumerable<Item> items, ItemStackType itemStackType)
        {
            return itemStackType switch
            {
                ItemStackType.Input => m_recipeRepository.FindAsInput(items),
                ItemStackType.Output => m_recipeRepository.FindAsOutput(items),
                ItemStackType.Any => m_recipeRepository.FindRelated(items),
                _ => throw new InvalidOperationException()
            };
        }
    }
}

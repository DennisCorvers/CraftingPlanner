using CraftingPlannerLib.Data;
using CraftingPlannerLib.Models;

namespace CraftingPlannerLib.Services
{
    public class ItemService
    {
        private readonly IItemRepository m_itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            m_itemRepository = itemRepository;
        }

        public IEnumerable<Item> GetItems()
            => m_itemRepository.Entities;

        public IEnumerable<Item> GetItems(string itemName)
            => m_itemRepository.Find(itemName);

        public IEnumerable<Item> Find(string itemNameLike)
            => m_itemRepository.NameContains(itemNameLike);

        public IEnumerable<Item> Find(string? itemNameLike, Mod? mod)
        {
            var filteredItems = m_itemRepository.Entities;

            if (mod != null)
            {
                filteredItems = mod.Items;
            }

            if (!string.IsNullOrEmpty(itemNameLike))
            {
                filteredItems = filteredItems.Where(x => x.Name.Contains(itemNameLike, StringComparison.OrdinalIgnoreCase));
            }

            return filteredItems;
        }
    }
}
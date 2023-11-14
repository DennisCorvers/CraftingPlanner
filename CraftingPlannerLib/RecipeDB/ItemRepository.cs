using DataImport.Models;

namespace CraftingPlannerLib.RecipeDB
{
    internal class ItemRepository : IItemRepository
    {
        private readonly IReadOnlyList<Item> m_items;
        private readonly ILookup<string, Item> m_nameIndex;

        public IEnumerable<Item> Entities
            => m_items;

        internal ItemRepository(IEnumerable<Item> items)
        {
            m_items = items
                .OrderBy(x => x.Name)
                .ToList();

            m_nameIndex = items
                .ToLookup(x => x.Name, StringComparer.OrdinalIgnoreCase);
        }

        public IEnumerable<Item> Find(string name)
            => m_nameIndex[name];

        public IEnumerable<Item> NameContains(string name)
            => Entities.Where(x => x.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
    }
}

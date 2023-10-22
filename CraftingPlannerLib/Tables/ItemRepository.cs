using CraftingPlannerLib.Entities;
using System.Diagnostics.CodeAnalysis;

namespace CraftingPlannerLib.Tables
{
    public class ItemRepository : Repository<Item>
    {
        public ItemRepository(Dictionary<int, Item> data)
            : base(data, new ItemEqualityComparer())
        {
        }

        private class ItemEqualityComparer : IEqualityComparer<Item>
        {
            public bool Equals(Item? x, Item? y)
            {
                if (ReferenceEquals(x, y))
                    return true;

                if (x is null || y is null)
                    return false;

                return StringComparer.OrdinalIgnoreCase.Equals(x.Name, y.Name);
            }

            public int GetHashCode([DisallowNull] Item obj)
                => obj.Name.GetHashCode();
        }

        public IEnumerable<Item> Filter(IEnumerable<IItemFilter> filters)
        {
            var filteredItems = base.GetAll();
            foreach (var filter in filters)
                filteredItems = filter.Filter(filteredItems);

            return filteredItems;
        }
    }
}

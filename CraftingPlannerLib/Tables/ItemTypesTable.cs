using CraftingPlannerLib.Entities;
using System.Diagnostics.CodeAnalysis;

namespace CraftingPlannerLib.Tables
{
    public class ItemTypesTable : BaseTable<ItemType>
    {
        public ItemTypesTable(Dictionary<int, ItemType> data)
            : base(data, new ItemTypeEqualityComparer())
        {
        }

        private class ItemTypeEqualityComparer : IEqualityComparer<ItemType>
        {
            public bool Equals(ItemType? x, ItemType? y)
            {
                if (ReferenceEquals(x, y))
                    return true;

                if (x is null || y is null)
                    return false;

                return StringComparer.OrdinalIgnoreCase.Equals(x.Name, y.Name);
            }

            public int GetHashCode([DisallowNull] ItemType obj)
                => obj.Name.GetHashCode();
        }
    }
}

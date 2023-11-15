using DataImport.Models;
using DataImport.Utils;
using System.Diagnostics.CodeAnalysis;

namespace DataImport.RecipeExporter.Comparers
{
    internal class ItemStackComparer : IEqualityComparer<ItemStack>
    {
        public static IEqualityComparer<ItemStack> Default { get; } = new ItemStackComparer();

        public bool Equals(ItemStack? x, ItemStack? y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (y is null || x is null)
                return false;

            // Test base values.
            if (x.ItemID != y.ItemID ||
                y.Amount != y.Amount)
            {
                return false;
            }

            if (ReferenceEquals(x.AlternativeItemIds, y.AlternativeItemIds))
                return true;

            if (x.AlternativeItemIds is null || y.AlternativeItemIds is null)
                return false;

            return x.AlternativeItemIds.SetEquals(y.AlternativeItemIds);
        }

        public int GetHashCode([DisallowNull] ItemStack obj)
        {
            var hash = HashCode.Combine(obj.ItemID, obj.Amount);

            if (obj.AlternativeItemIds != null)
                return HashCode.Combine(hash, HashUtilities.GetEnumerableHash(obj.AlternativeItemIds.OrderBy(x => x)));

            return hash;
        }
    }
}

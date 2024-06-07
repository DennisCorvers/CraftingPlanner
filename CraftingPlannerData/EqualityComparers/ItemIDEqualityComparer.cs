using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using CraftingPlannerLib.Models;

namespace CraftingPlannerData.EqualityComparers
{
    /// <summary>
    /// Comparer used to create (Item -> Recipe) indexes
    /// </summary>
    internal class ItemIDEqualityComparer : IEqualityComparer<Item>
    {
        public static IEqualityComparer<Item> Default { get; } = new ItemIDEqualityComparer();

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

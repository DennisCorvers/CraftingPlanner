using System.Diagnostics.CodeAnalysis;
using CraftingPlannerLib.Utils;

namespace DataImport.RecipeExporter.Comparers
{
    /// <summary>
    /// Comparer that checks if a Recipe is equal for the context of this application.
    /// This will result in a uniqueness of recipes for functional use regardless of
    /// the imported data.
    /// </summary>
    internal class RecipeComparer : IEqualityComparer<Recipe>
    {
        public static IEqualityComparer<Recipe> Default { get; } = new RecipeComparer();

        public bool Equals(Recipe? x, Recipe? y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (y is null || x is null)
                return false;

            if (x.Input.Count != y.Input.Count)
                return false;

            var stackComparer = ItemStackComparer.Default;

            var xOrdered = x.Input.OrderBy(x => x.ItemID);
            var yOrdered = y.Input.OrderBy(y => y.ItemID);

            return stackComparer.Equals(x.Output, y.Output) &&
                xOrdered.SequenceEqual(yOrdered, stackComparer);
        }

        public int GetHashCode([DisallowNull] Recipe obj)
        {
            var stackComparer = ItemStackComparer.Default;
            var orderedInputs = obj.Input.OrderBy(x => x.ItemID);

            var inputHash = HashUtilities.GetEnumerableHash(orderedInputs, stackComparer);

            return HashCode.Combine(inputHash, stackComparer.GetHashCode(obj.Output));
        }
    }
}

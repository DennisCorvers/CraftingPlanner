using DataImport.Models;
using System.Diagnostics.CodeAnalysis;

namespace DataImport.EqualityComparers
{
    internal class RecipeComparer : IEqualityComparer<Recipe>
    {
        public IEqualityComparer<Recipe> Default { get; } = new RecipeComparer();

        public bool Equals(Recipe? x, Recipe? y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (y is null || x is null)
                return false;

            return x.Output.Equals(y.Output)
                && x.Input.SequenceEqual(y.Input);
        }

        public int GetHashCode([DisallowNull] Recipe obj)
        {
            return HashCode.Combine(obj.Output, ListHash(obj.Input));
        }

        private static int ListHash(IEnumerable<ItemStack> items)
        {
            unchecked
            {
                int hash = 19;
                foreach (var item in items)
                {
                    hash = hash * 31 + item.GetHashCode();
                }
                return hash;
            }
        }
    }
}

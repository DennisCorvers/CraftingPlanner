using CraftingPlannerLib.Entities;
using System.Diagnostics.CodeAnalysis;

namespace CraftingPlannerLib.Tables
{
    public class ModsTable : BaseTable<Mod>
    {
        public ModsTable(Dictionary<int, Mod> data)
            : base(data, new ModEqualityComparer())
        {
        }

        private class ModEqualityComparer : IEqualityComparer<Mod>
        {
            public bool Equals(Mod? x, Mod? y)
            {
                if (ReferenceEquals(x, y))
                    return true;

                if (x is null || y is null)
                    return false;

                return StringComparer.OrdinalIgnoreCase.Equals(x.Name, y.Name);
            }

            public int GetHashCode([DisallowNull] Mod obj)
                => obj.Name.GetHashCode();
        }
    }
}

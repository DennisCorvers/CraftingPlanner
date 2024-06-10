using CraftingPlannerLib.Data.GameData;
using CraftingPlannerLib.Models;

namespace CraftingPlannerData.Repositories
{
    internal class ModRepository : IModRepository
    {
        private readonly IReadOnlyList<Mod> m_mods;
        private readonly IDictionary<string, Mod> m_nameIndex;

        public IEnumerable<Mod> Entities
            => m_mods;

        internal ModRepository(IEnumerable<Mod> mods)
        {
            m_mods = mods
                .OrderBy(m => m.Name)
                .ToList();

            m_nameIndex = mods
                .ToDictionary(k => k.Name, m => m, StringComparer.OrdinalIgnoreCase);
        }

        public Mod? Find(string name)
        {
            m_nameIndex.TryGetValue(name, out Mod? mod);
            return mod;
        }
    }
}

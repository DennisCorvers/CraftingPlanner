using CraftingPlannerLib.DAL;
using DataImport.Models;

namespace CraftingPlannerLib.RecipeDB
{
    internal class ModRepository : IModRepository
    {
        private readonly IReadOnlyList<Mod> m_mods;
        private readonly IDictionary<string, Mod> m_modLookup;

        public IEnumerable<Mod> Entities
            => m_mods;

        internal ModRepository(IEnumerable<Mod> mods)
        {
            m_mods = mods
                .OrderBy(m => m.Name)
                .ToList();

            m_modLookup = mods.ToDictionary(k => k.Name, m => m, StringComparer.OrdinalIgnoreCase);
        }

        public Mod? Find(string name)
        {
            m_modLookup.TryGetValue(name, out Mod? mod);
            return mod;
        }
    }
}

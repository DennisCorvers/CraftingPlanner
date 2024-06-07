using CraftingPlannerLib.Data;
using CraftingPlannerLib.Models;

namespace CraftingPlannerLib.RecipeDB.Services
{
    public class ModService
    {
        private readonly IModRepository m_modRepository;

        public ModService(IModRepository modRepository)
        {
            m_modRepository = modRepository;
        }

        public IEnumerable<Mod> GetMods()
            => m_modRepository.Entities;
    }
}

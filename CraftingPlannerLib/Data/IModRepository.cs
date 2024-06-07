using CraftingPlannerLib.Models;

namespace CraftingPlannerLib.Data
{
    public interface IModRepository : IReadonlyRepository<Mod>
    {
        Mod? Find(string name);
    }
}

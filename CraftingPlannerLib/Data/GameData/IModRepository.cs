using CraftingPlannerLib.Models;

namespace CraftingPlannerLib.Data.GameData
{
    public interface IModRepository : IReadonlyRepository<Mod>
    {
        Mod? Find(string name);
    }
}

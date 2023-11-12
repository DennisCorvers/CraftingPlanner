using CraftingPlannerLib.DAL;
using DataImport.Models;

namespace CraftingPlannerLib.RecipeDB
{
    public interface IModRepository : IReadonlyRepository<Mod>
    {
        Mod? Find(string name);
    }
}

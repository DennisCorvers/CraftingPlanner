using CraftingPlannerLib.DAL;
using DataImport.Models;

namespace CraftingPlannerLib.RecipeDB
{
    public interface IItemRepository : IReadonlyRepository<Item>
    {
        IEnumerable<Item> Find(string name);
        IEnumerable<Item> NameContains(string name);
    }
}

using DataImport.Models;

namespace CraftingPlannerLib.DAL
{
    public interface IReadonlyRepository<T> where T : class
    {
        IEnumerable<T> Entities { get; }
    }
}

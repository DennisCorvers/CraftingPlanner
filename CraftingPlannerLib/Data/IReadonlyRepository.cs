

namespace CraftingPlannerLib.Data
{
    public interface IReadonlyRepository<T> where T : class
    {
        IEnumerable<T> Entities { get; }
    }
}

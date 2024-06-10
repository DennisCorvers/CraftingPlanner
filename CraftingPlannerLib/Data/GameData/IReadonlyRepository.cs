namespace CraftingPlannerLib.Data.GameData
{
    public interface IReadonlyRepository<T> where T : class
    {
        IEnumerable<T> Entities { get; }
    }
}

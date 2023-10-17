using CraftingPlannerLib.Entities;

namespace CraftingPlannerLib.Tables
{
    public class BaseTable<T> where T : Entity
    {
        public int NextUID { get; }

        public Dictionary<int, T> Data { get; }

        public IEnumerable<T> Values
            => Data.Values;

        private HashSet<T> Lookup { get; }

        public BaseTable(IEqualityComparer<T> valueComparer)
            : this(new Dictionary<int, T>(), valueComparer)
        { }

        public BaseTable(Dictionary<int, T> data, IEqualityComparer<T> valueComparer)
        {
            NextUID = data.Count == 0 ? 0 : data.Max(x => x.Key) + 1;
            Data = data;
            Lookup = Data.Values.ToHashSet(valueComparer);
        }

        public bool TryAdd(T entity)
        {
            if (Lookup.Add(entity))
            {
                Data.Add(entity.Id, entity);
                return true;
            }

            return false;
        }

        public bool TryRemove(int id)
            => Data.TryGetValue(id, out T? entity) && TryRemove(entity);

        public bool TryRemove(T entity)
            => Data.Remove(entity.Id) && Lookup.Remove(entity);
    }
}

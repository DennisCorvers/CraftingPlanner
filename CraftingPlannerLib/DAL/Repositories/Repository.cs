using CraftingPlannerLib.DAL.Entities;
using System.Runtime.CompilerServices;

namespace CraftingPlannerLib.DAL.Repositories
{
    public class Repository<T> where T : NamedEntity
    {
        private int m_nextUID;

        private Dictionary<int, T> m_data;

        private HashSet<T> m_lookup;

        public Repository(IEqualityComparer<T> valueComparer)
            : this(new Dictionary<int, T>(), valueComparer)
        { }

        public Repository(Dictionary<int, T> data, IEqualityComparer<T> valueComparer)
        {
            m_nextUID = data.Count == 0 ? 0 : data.Max(x => x.Key) + 1;
            m_data = data;
            m_lookup = m_data.Values.ToHashSet(valueComparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T? Get(int id)
            => m_data.TryGetValue(id, out T? value) ? value : null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetAll()
            => m_data.Values;

        public bool Add(T entity)
        {
            if (entity.Id >= 0)
                throw new ArgumentException("Cannot add an existing entity.", nameof(entity));

            if (m_lookup.Add(entity))
            {
                entity.Id = m_nextUID++;
                m_data.Add(entity.Id, entity);
                return true;
            }

            return false;
        }

        public bool TryAdd(T entity)
        {
            if (m_lookup.Add(entity))
            {
                m_data.Add(entity.Id, entity);
                return true;
            }

            return false;
        }

        public bool Remove(int id)
            => m_data.TryGetValue(id, out T? entity) && Remove(entity);

        public bool Remove(T entity)
            => m_data.Remove(entity.Id) && m_lookup.Remove(entity);
    }
}

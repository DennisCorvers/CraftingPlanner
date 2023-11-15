namespace DataImport.Utils
{
    internal static class HashUtilities
    {
        public static int GetEnumerableHash<T>(IEnumerable<T> values)
            where T : notnull
        {
            return GetEnumerableHash(values, EqualityComparer<T>.Default);
        }

        public static int GetEnumerableHash<T>(IEnumerable<T> values, IEqualityComparer<T> equalityComparer)
            where T : notnull
        {
            unchecked
            {
                int hash = 19;
                foreach (var obj in values)
                {
                    hash = hash * 31 + equalityComparer.GetHashCode(obj);
                }
                return hash;
            }
        }
    }
}

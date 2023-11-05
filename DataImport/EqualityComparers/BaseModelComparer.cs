using DataImport.Models;
using System.Diagnostics.CodeAnalysis;

namespace DataImport.EqualityComparers
{
    internal class BaseModelComparer : IEqualityComparer<BaseModel>
    {
        public static IEqualityComparer<BaseModel> Default { get; } = new BaseModelComparer();

        public bool Equals(BaseModel? x, BaseModel? y)
        {
            if (ReferenceEquals(x, y))
                return true;

            if (y is null || x is null)
                return false;

            return x.ID == y.ID;
        }

        public int GetHashCode([DisallowNull] BaseModel obj) => obj.ID;
    }
}

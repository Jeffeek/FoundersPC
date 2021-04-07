using System;
using System.Linq;

namespace FoundersPC.ApplicationShared.Collections
{
    public static class CollectionsExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        {
            if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageNumber));
            if (pageNumber <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize));

            return source.Skip(pageSize * (pageNumber - 1))
                         .Take(pageSize);
        }
    }
}

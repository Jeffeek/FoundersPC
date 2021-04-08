#region Using namespaces

using System;
using System.Linq;
using FoundersPC.RepositoryShared.Identity;

#endregion

namespace FoundersPC.ApplicationShared.Collections
{
    public static class CollectionsExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> source, int pageNumber, int pageSize)
            where T : IIdentityItem
        {
            if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageNumber));
            if (pageNumber <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize));

            return source.OrderBy(x => x.Id)
                         .Skip(pageSize * (pageNumber - 1))
                         .Take(pageSize);
        }
    }
}
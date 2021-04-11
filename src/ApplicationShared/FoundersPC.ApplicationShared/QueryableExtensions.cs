#region Using namespaces

using System;
using System.Linq;
using FoundersPC.IdentityEntities.Identity;

#endregion

namespace FoundersPC.ApplicationShared
{
    public static class QueryableExtensions
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
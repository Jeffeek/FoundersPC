#region Using namespaces

using System;
using System.Linq;
using FoundersPC.IdentityEntities.Identity;

#endregion

namespace FoundersPC.ApplicationShared
{
    public static class QueryableExtensions
    {
        /// <exception cref="T:System.ArgumentOutOfRangeException">pageNumber or pageSize was below or equal to 0.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> or <paramref name="keySelector" /> is <see langword="null" />.</exception>
        public static IQueryable<T> Paginate<T>(this IQueryable<T> source, int pageNumber, int pageSize)
            where T : IIdentityItem
        {
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageNumber));

            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize));

            return source.OrderBy(x => x.Id)
                         .Skip(pageSize * (pageNumber - 1))
                         .Take(pageSize);
        }
    }
}
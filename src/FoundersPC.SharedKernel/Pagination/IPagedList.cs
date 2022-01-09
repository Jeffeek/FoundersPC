#region Using namespaces

using System.Collections.Generic;

#endregion

namespace FoundersPC.SharedKernel.Pagination;

public interface IPagedList<T> : IEnumerable<T>
{
    PagingInfo PagingInfo { get; }

    List<T> Result { get; }
}
using System.Collections.Generic;

namespace FoundersPC.SharedKernel.Pagination
{
    public interface IPagedList<T>
    {
        PagingInfo PagingInfo { get; }

        List<T> Result { get; }
    }
}
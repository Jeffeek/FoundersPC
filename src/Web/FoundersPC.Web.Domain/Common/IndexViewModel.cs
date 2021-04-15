#region Using namespaces

using System.Collections.Generic;
using PagedList.Core;

#endregion

namespace FoundersPC.Web.Domain.Common
{
    public class IndexViewModel<T> where T : class
    {
        public IPagedList<T> PagedList { get; set; }

        public bool IsPaginationNeeded { get; set; }
    }
}
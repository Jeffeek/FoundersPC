#region Using namespaces

using System.Collections.Generic;

#endregion

namespace FoundersPC.Web.Domain.Common
{
    public class IndexViewModel<T> where T : class
    {
        public PageViewModel Page { get; set; }

        public IEnumerable<T> Models { get; set; }

        public bool IsPaginationNeeded { get; set; }
    }
}
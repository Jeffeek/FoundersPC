using System.Collections.Generic;

namespace FoundersPC.Web.Models
{
    public class IndexViewModel<T> where T : class
    {
        public PageViewModel Page { get; set; }

        public IEnumerable<T> Models { get; set; }
    }
}

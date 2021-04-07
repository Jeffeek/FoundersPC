using System;

namespace FoundersPC.Web.Models
{
    public class PageViewModel
    {
        public int CurrentPageNumber { get; }

        public PageViewModel(int currentPageNumber, bool hasNextPage)
        {
            CurrentPageNumber = currentPageNumber;
            HasPreviousPage = CurrentPageNumber > 1;
            HasNextPage = hasNextPage;
        }

        public bool HasPreviousPage { get; }

        public bool HasNextPage { get; }
    }
}

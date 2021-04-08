namespace FoundersPC.Web.Domain.Common
{
    public class PageViewModel
    {
        public PageViewModel(int currentPageNumber, bool hasNextPage)
        {
            CurrentPageNumber = currentPageNumber;
            HasPreviousPage = CurrentPageNumber > 1;
            HasNextPage = hasNextPage;
        }

        public int CurrentPageNumber { get; }

        public bool HasPreviousPage { get; }

        public bool HasNextPage { get; }
    }
}
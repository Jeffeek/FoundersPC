namespace FoundersPC.SharedKernel.Filter;

public abstract class SortedPagedFilter : IPagedFilter, ISortedFilter
{
    protected SortedPagedFilter()
    {
        PageNumber = DefaultConstants.DefaultPageNumber;
        PageSize = DefaultConstants.DefaultPageSize;
        SortColumn = DefaultConstants.DefaultSortColumn;
        IsAscending = DefaultConstants.DefaultIsAscending;
    }

    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public string SortColumn { get; set; }

    public bool IsAscending { get; set; }
}
namespace FoundersPC.SharedKernel.Filter;

public abstract class SortedFilter : ISortedFilter
{
    protected SortedFilter()
    {
        SortColumn = DefaultConstants.DefaultSortColumn;
        IsAscending = DefaultConstants.DefaultIsAscending;
    }

    public string SortColumn { get; set; }

    public bool IsAscending { get; set; }
}
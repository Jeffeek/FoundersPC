namespace FoundersPC.SharedKernel.Filter
{
    public interface ISortedFilter
    {
        string SortColumn { get; set; }

        bool IsAscending { get; set; }
    }
}
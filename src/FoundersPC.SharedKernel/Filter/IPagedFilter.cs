namespace FoundersPC.SharedKernel.Filter
{
    public interface IPagedFilter
    {
        int PageNumber { get; set; }

        int PageSize { get; set; }
    }
}
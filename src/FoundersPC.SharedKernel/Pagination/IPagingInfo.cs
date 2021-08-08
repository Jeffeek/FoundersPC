namespace FoundersPC.SharedKernel.Pagination
{
    public interface IPagingInfo
    {
        int PageNumber { get; set; }

        int PageSize { get; set; }

        int TotalItemCount { get; set; }

        int PageCount { get; }
    }
}
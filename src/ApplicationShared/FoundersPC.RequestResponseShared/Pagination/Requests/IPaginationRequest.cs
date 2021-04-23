namespace FoundersPC.RequestResponseShared.Pagination.Requests
{
    public interface IPaginationRequest
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
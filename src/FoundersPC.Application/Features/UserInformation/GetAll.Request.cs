using FoundersPC.Application.Features.UserInformation.Models;
using FoundersPC.SharedKernel.Filter;
using FoundersPC.SharedKernel.Pagination;
using MediatR;

namespace FoundersPC.Application.Features.UserInformation;

public class GetAllRequest : SortedPagedFilter, IRequest<IPagedList<UserViewInfo>>
{
    public bool? ShowBlocked { get; set; }
    public string? SearchText { get; set; }
}
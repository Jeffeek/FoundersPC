using FoundersPC.Application.Features.Client.Producer.Models;
using FoundersPC.SharedKernel.Filter;
using FoundersPC.SharedKernel.Pagination;
using MediatR;

namespace FoundersPC.Application.Features.Client.Producer;

public class GetAllRequest : SortedPagedFilter, IRequest<IPagedList<ClientProducerInfo>>
{
    public string? FreeText { get; set; }
}
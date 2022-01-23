using FoundersPC.Application.Features.Producer.Models;
using FoundersPC.SharedKernel.Filter;
using FoundersPC.SharedKernel.Pagination;
using MediatR;

namespace FoundersPC.Application.Features.Producer;

public class GetAllRequest : SortedPagedFilter, IRequest<IPagedList<ProducerViewInfo>>
{
    public string? FreeText { get; set; }
    public bool? ShowDeleted { get; set; }
}
using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Client.Hardware.RandomAccessMemory;
using FoundersPC.Application.Features.Client.Hardware.RandomAccessMemory.Models;
using FoundersPC.SharedKernel.Endpoints;
using FoundersPC.SharedKernel.Pagination;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Client.Hardware.RandomAccessMemory;

public class GetAllEndpoint : BaseRequestResponseAccessTokenEndpoint<GetAllRequest, IPagedList<ClientRandomAccessMemoryInfo>, GetEndpoint>
{
    [HttpPost("Hardware/RandomAccessMemory/GetAll")]
    [OpenApiOperation(operationId : "RandomAccessMemory.GetAll",
                      summary : "Get all RandomAccessMemory",
                      description : "Get all RandomAccessMemory")]
    [OpenApiTags("Hardware", "RandomAccessMemory", "Client")]
    public override async Task<ActionResult<IPagedList<ClientRandomAccessMemoryInfo>>> HandleAsync(GetAllRequest request, CancellationToken cancellationToken) =>
        await base.HandleAsync(request, cancellationToken);
}
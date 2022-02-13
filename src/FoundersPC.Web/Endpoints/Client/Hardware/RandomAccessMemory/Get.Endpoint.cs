using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Client.Hardware.RandomAccessMemory;
using FoundersPC.Application.Features.Client.Hardware.RandomAccessMemory.Models;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Client.Hardware.RandomAccessMemory;

public class GetEndpoint : BaseRequestResponseAccessTokenEndpoint<GetRequest, ClientRandomAccessMemoryInfo, GetEndpoint>
{
    [HttpGet("Hardware/RandomAccessMemory/{Id:int:required}")]
    [OpenApiOperation(operationId : "Hardware.RandomAccessMemory.Get",
                      summary : "Get Hardware.RandomAccessMemory",
                      description : "Get Hardware.RandomAccessMemory")]
    [OpenApiTags("Hardware", "RandomAccessMemory", "Client")]
    public override async Task<ClientRandomAccessMemoryInfo> HandleAsync([FromRoute] GetRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}
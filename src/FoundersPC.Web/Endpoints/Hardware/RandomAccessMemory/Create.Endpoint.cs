using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.RandomAccessMemory;
using FoundersPC.Application.Features.Hardware.RandomAccessMemory.Models;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.RandomAccessMemory;

public class CreateEndpoint : BaseRequestResponseManagementEndpoint<CreateRequest, RandomAccessMemoryInfo, GetEndpoint>
{
    [HttpPost("Hardware/RandomAccessMemory")]
    [OpenApiOperation(operationId : "Hardware.RandomAccessMemory.Post",
                      summary : "Create Hardware.RandomAccessMemory",
                      description : "Create Hardware.RandomAccessMemory")]
    [OpenApiTags("Hardware", "RandomAccessMemory")]
    public override async Task<RandomAccessMemoryInfo> HandleAsync([FromBody] CreateRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}
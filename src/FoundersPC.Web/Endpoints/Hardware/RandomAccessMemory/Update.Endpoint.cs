using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.RandomAccessMemory;
using FoundersPC.Application.Features.Hardware.RandomAccessMemory.Models;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.RandomAccessMemory;

public class UpdateEndpoint : BaseRequestResponseManagementEndpoint<UpdateRequest, RandomAccessMemoryInfo, GetEndpoint>
{
    [HttpPut("Hardware/RandomAccessMemory")]
    [OpenApiOperation(operationId : "Hardware.RandomAccessMemory.Put",
                      summary : "Update Hardware.RandomAccessMemory",
                      description : "Update Hardware.RandomAccessMemory")]
    [OpenApiTags("Hardware", "RandomAccessMemory")]
    public override async Task<ActionResult<RandomAccessMemoryInfo>> HandleAsync([FromBody] UpdateRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}
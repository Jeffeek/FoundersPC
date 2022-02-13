using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.RandomAccessMemory;
using FoundersPC.Web.Endpoints.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.RandomAccessMemory;

public class DeleteEndpoint : BaseRequestResponseManagementEndpoint<DeleteRequest, Unit, GetEndpoint>
{
    [HttpDelete("Hardware/RandomAccessMemory")]
    [OpenApiOperation(operationId : "Hardware.RandomAccessMemory.Delete",
                      summary : "Delete Hardware.RandomAccessMemory",
                      description : "Delete Hardware.RandomAccessMemory")]
    [OpenApiTags("Hardware", "RandomAccessMemory")]
    public override async Task<Unit> HandleAsync([FromBody] DeleteRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}
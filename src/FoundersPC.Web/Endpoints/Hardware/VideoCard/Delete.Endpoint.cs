using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.VideoCard;
using FoundersPC.Web.Endpoints.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.VideoCard;

public class DeleteEndpoint : BaseRequestResponseManagementEndpoint<DeleteRequest, Unit, GetEndpoint>
{
    [HttpDelete("Hardware/VideoCard")]
    [OpenApiOperation(operationId : "Hardware.VideoCard.Delete",
                      summary : "Delete Hardware.VideoCard",
                      description : "Delete Hardware.VideoCard")]
    [OpenApiTags("Hardware", "VideoCard")]
    public override async Task<Unit> HandleAsync([FromBody] DeleteRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}
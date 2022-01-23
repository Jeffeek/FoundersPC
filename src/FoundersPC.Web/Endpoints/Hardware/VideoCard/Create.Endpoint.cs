using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.VideoCard;
using FoundersPC.Application.Features.Hardware.VideoCard.Models;
using FoundersPC.SharedKernel.Endpoints;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.VideoCard;

public class CreateEndpoint : BaseRequestResponseManagementEndpoint<CreateRequest, VideoCardInfo, Case.GetEndpoint>
{
    [HttpPost("Hardware/VideoCard")]
    [OpenApiOperation(operationId : "Hardware.VideoCard.Post",
                      summary : "Create Hardware.VideoCard",
                      description : "Create Hardware.VideoCard")]
    [OpenApiTags("Hardware", "VideoCard")]
    public override async Task<ActionResult<VideoCardInfo>> HandleAsync([FromBody] CreateRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}
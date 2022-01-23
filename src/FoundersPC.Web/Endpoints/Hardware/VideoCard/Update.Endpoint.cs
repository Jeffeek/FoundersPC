using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.VideoCard;
using FoundersPC.Application.Features.Hardware.VideoCard.Models;
using FoundersPC.SharedKernel.Endpoints;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.VideoCard;

public class UpdateEndpoint : BaseRequestResponseManagementEndpoint<UpdateRequest, VideoCardInfo, GetEndpoint>
{
    [HttpPut("Hardware/VideoCard")]
    [OpenApiOperation(operationId : "Hardware.VideoCard.Put",
                      summary : "Update Hardware.VideoCard",
                      description : "Update Hardware.VideoCard")]
    [OpenApiTags("Hardware", "VideoCard")]
    public override async Task<ActionResult<VideoCardInfo>> HandleAsync([FromBody] UpdateRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}
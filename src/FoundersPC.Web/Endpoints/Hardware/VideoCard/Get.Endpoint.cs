using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.VideoCard;
using FoundersPC.Application.Features.Hardware.VideoCard.Models;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.VideoCard;

public class GetEndpoint : BaseRequestResponseManagementEndpoint<GetRequest, VideoCardInfo, GetEndpoint>
{
    [HttpGet("Hardware/VideoCard/{Id:int:required}")]
    [OpenApiOperation(operationId : "Hardware.VideoCard.Get",
                      summary : "Get Hardware.VideoCard",
                      description : "Get Hardware.VideoCard")]
    [OpenApiTags("Hardware", "VideoCard")]
    public override async Task<VideoCardInfo> HandleAsync([FromRoute] GetRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}
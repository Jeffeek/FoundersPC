using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Client.Hardware.VideoCard;
using FoundersPC.Application.Features.Client.Hardware.VideoCard.Models;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Client.Hardware.VideoCard;

public class GetEndpoint : BaseRequestResponseAccessTokenEndpoint<GetRequest, ClientVideoCardInfo, GetEndpoint>
{
    [HttpGet("Hardware/VideoCard/{Id:int:required}")]
    [OpenApiOperation(operationId : "Hardware.VideoCard.Get",
                      summary : "Get Hardware.VideoCard",
                      description : "Get Hardware.VideoCard")]
    [OpenApiTags("Hardware", "VideoCard", "Client")]
    public override async Task<ClientVideoCardInfo> HandleAsync([FromRoute] GetRequest request, CancellationToken cancellationToken = new()) =>
        await Mediator.Send(request, cancellationToken);
}
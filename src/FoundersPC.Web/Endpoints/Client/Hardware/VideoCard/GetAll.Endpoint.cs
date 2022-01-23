using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Client.Hardware.VideoCard;
using FoundersPC.Application.Features.Client.Hardware.VideoCard.Models;
using FoundersPC.SharedKernel.Endpoints;
using FoundersPC.SharedKernel.Pagination;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Client.Hardware.VideoCard;

public class GetAllEndpoint : BaseRequestResponseAccessTokenEndpoint<GetAllRequest, IPagedList<ClientVideoCardInfo>, GetEndpoint>
{
    [HttpPost("Hardware/VideoCard/GetAll")]
    [OpenApiOperation(operationId : "VideoCard.GetAll",
                      summary : "Get all VideoCards",
                      description : "Get all VideoCards")]
    [OpenApiTags("Hardware", "VideoCard", "Client")]
    public override async Task<ActionResult<IPagedList<ClientVideoCardInfo>>> HandleAsync(GetAllRequest request, CancellationToken cancellationToken) =>
        await base.HandleAsync(request, cancellationToken);
}
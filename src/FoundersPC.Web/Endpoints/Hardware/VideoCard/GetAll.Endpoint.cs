using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.VideoCard;
using FoundersPC.Application.Features.Hardware.VideoCard.Models;
using FoundersPC.SharedKernel.Pagination;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.VideoCard;

public class GetAllEndpoint : BaseRequestResponseManagementEndpoint<GetAllRequest, IPagedList<VideoCardViewInfo>, GetEndpoint>
{
    [HttpPost("Hardware/VideoCard/GetAll")]
    [OpenApiOperation(operationId : "VideoCard.GetAll",
                      summary : "Get all VideoCards",
                      description : "Get all VideoCards")]
    [OpenApiTags("Hardware", "VideoCard")]
    public override async Task<IPagedList<VideoCardViewInfo>> HandleAsync([FromBody] GetAllRequest request, CancellationToken cancellationToken = default) =>
        await base.HandleAsync(request, cancellationToken);
}
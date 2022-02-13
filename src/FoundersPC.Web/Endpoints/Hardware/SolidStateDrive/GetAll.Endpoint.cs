using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.SolidStateDrive;
using FoundersPC.Application.Features.Hardware.SolidStateDrive.Models;
using FoundersPC.SharedKernel.Pagination;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.SolidStateDrive;

public class GetAllEndpoint : BaseRequestResponseManagementEndpoint<GetAllRequest, IPagedList<SolidStateDriveViewInfo>, GetEndpoint>
{
    [HttpPost("Hardware/SolidStateDrive/GetAll")]
    [OpenApiOperation(operationId : "SolidStateDrive.GetAll",
                      summary : "Get all SolidStateDrives",
                      description : "Get all SolidStateDrives")]
    [OpenApiTags("Hardware", "SolidStateDrive")]
    public override async Task<IPagedList<SolidStateDriveViewInfo>> HandleAsync([FromBody] GetAllRequest request, CancellationToken cancellationToken = default) =>
        await base.HandleAsync(request, cancellationToken);
}
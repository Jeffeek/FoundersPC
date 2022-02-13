using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.HardDriveDisk;
using FoundersPC.Application.Features.Hardware.HardDriveDisk.Models;
using FoundersPC.SharedKernel.Pagination;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.HardDriveDisk;

public class GetAllEndpoint : BaseRequestResponseManagementEndpoint<GetAllRequest, IPagedList<HardDriveDiskViewInfo>, GetEndpoint>
{
    [HttpPost("Hardware/HardDriveDisk/GetAll")]
    [OpenApiOperation(operationId : "HardDriveDisk.GetAll",
                      summary : "Get all HardDriveDisks",
                      description : "Get all HardDriveDisks")]
    [OpenApiTags("Hardware", "HardDriveDisk")]
    public override async Task<IPagedList<HardDriveDiskViewInfo>> HandleAsync([FromBody] GetAllRequest request, CancellationToken cancellationToken = default) =>
        await base.HandleAsync(request, cancellationToken);
}
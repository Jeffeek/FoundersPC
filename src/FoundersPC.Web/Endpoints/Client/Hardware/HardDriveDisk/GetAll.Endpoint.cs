using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Client.Hardware.HardDriveDisk;
using FoundersPC.Application.Features.Client.Hardware.HardDriveDisk.Models;
using FoundersPC.SharedKernel.Pagination;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Client.Hardware.HardDriveDisk;

public class GetAllEndpoint : BaseRequestResponseAccessTokenEndpoint<GetAllRequest, IPagedList<ClientHardDriveDiskInfo>, GetEndpoint>
{
    [HttpPost("Hardware/HardDriveDisk/GetAll")]
    [OpenApiOperation(operationId : "Client.HardDriveDisk.GetAll",
                      summary : "Get all Client.HardDriveDisks",
                      description : "Get all Client.HardDriveDisks")]
    [OpenApiTags("Hardware", "HardDriveDisk", "Client")]
    public override async Task<ActionResult<IPagedList<ClientHardDriveDiskInfo>>> HandleAsync(GetAllRequest request, CancellationToken cancellationToken) =>
        await base.HandleAsync(request, cancellationToken);
}
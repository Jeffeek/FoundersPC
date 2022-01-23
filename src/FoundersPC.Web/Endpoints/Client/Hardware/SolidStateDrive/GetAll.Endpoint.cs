using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Client.Hardware.SolidStateDrive;
using FoundersPC.Application.Features.Client.Hardware.SolidStateDrive.Models;
using FoundersPC.SharedKernel.Endpoints;
using FoundersPC.SharedKernel.Pagination;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Client.Hardware.SolidStateDrive;

public class GetAllEndpoint : BaseRequestResponseAccessTokenEndpoint<GetAllRequest, IPagedList<ClientSolidStateDriveInfo>, GetEndpoint>
{
    [HttpPost("Hardware/SolidStateDrive/GetAll")]
    [OpenApiOperation(operationId : "SolidStateDrive.GetAll",
                      summary : "Get all SolidStateDrives",
                      description : "Get all SolidStateDrives")]
    [OpenApiTags("Hardware", "SolidStateDrive", "Client")]
    public override async Task<ActionResult<IPagedList<ClientSolidStateDriveInfo>>> HandleAsync(GetAllRequest request, CancellationToken cancellationToken) =>
        await base.HandleAsync(request, cancellationToken);
}
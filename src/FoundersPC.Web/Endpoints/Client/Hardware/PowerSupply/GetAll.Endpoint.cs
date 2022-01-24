using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Client.Hardware.PowerSupply;
using FoundersPC.Application.Features.Client.Hardware.PowerSupply.Models;
using FoundersPC.SharedKernel.Pagination;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Client.Hardware.PowerSupply;

public class GetAllEndpoint : BaseRequestResponseAccessTokenEndpoint<GetAllRequest, IPagedList<ClientPowerSupplyInfo>, GetEndpoint>
{
    [HttpPost("Hardware/PowerSupply/GetAll")]
    [OpenApiOperation(operationId : "Client.PowerSupply.GetAll",
                      summary : "Get all Client.PowerSupplies",
                      description : "Get all Client.PowerSupplies")]
    [OpenApiTags("Hardware", "PowerSupply", "Client")]
    public override async Task<ActionResult<IPagedList<ClientPowerSupplyInfo>>> HandleAsync(GetAllRequest request, CancellationToken cancellationToken) =>
        await base.HandleAsync(request, cancellationToken);
}
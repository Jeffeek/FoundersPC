using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.PowerSupply;
using FoundersPC.Application.Features.Hardware.PowerSupply.Models;
using FoundersPC.SharedKernel.Pagination;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.PowerSupply;

public class GetAllEndpoint : BaseRequestResponseManagementEndpoint<GetAllRequest, IPagedList<PowerSupplyViewInfo>, GetEndpoint>
{
    [HttpPost("Hardware/PowerSupply/GetAll")]
    [OpenApiOperation(operationId : "PowerSupply.GetAll",
                      summary : "Get all PowerSupplies",
                      description : "Get all PowerSupplies")]
    [OpenApiTags("Hardware", "PowerSupply")]
    public override async Task<IPagedList<PowerSupplyViewInfo>> HandleAsync([FromBody] GetAllRequest request, CancellationToken cancellationToken = default) =>
        await base.HandleAsync(request, cancellationToken);
}
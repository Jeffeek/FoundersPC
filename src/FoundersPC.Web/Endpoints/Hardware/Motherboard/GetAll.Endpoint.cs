using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.Motherboard;
using FoundersPC.Application.Features.Hardware.Motherboard.Models;
using FoundersPC.SharedKernel.Pagination;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.Motherboard;

public class GetAllEndpoint : BaseRequestResponseManagementEndpoint<GetAllRequest, IPagedList<MotherboardViewInfo>, GetEndpoint>
{
    [HttpPost("Hardware/Motherboard/GetAll")]
    [OpenApiOperation(operationId : "Motherboard.GetAll",
                      summary : "Get all Motherboards",
                      description : "Get all Motherboards")]
    [OpenApiTags("Hardware", "Motherboard")]
    public override async Task<IPagedList<MotherboardViewInfo>> HandleAsync([FromBody] GetAllRequest request, CancellationToken cancellationToken = default) =>
        await base.HandleAsync(request, cancellationToken);
}
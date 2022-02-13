using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Hardware.Motherboard;
using FoundersPC.Application.Features.Hardware.Motherboard.Models;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Hardware.Motherboard;

public class CreateEndpoint : BaseRequestResponseManagementEndpoint<CreateRequest, MotherboardInfo, GetEndpoint>
{
    [HttpPost("Hardware/Motherboard")]
    [OpenApiOperation(operationId : "Hardware.Motherboard.Post",
                      summary : "Create Hardware.Motherboard",
                      description : "Create Hardware.Motherboard")]
    [OpenApiTags("Hardware", "Motherboard")]
    public override async Task<MotherboardInfo> HandleAsync([FromBody] CreateRequest request, CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(request, cancellationToken);
}
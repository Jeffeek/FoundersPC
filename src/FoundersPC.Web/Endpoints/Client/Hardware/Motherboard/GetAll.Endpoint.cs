using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Client.Hardware.Motherboard;
using FoundersPC.Application.Features.Client.Hardware.Motherboard.Models;
using FoundersPC.SharedKernel.Pagination;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Client.Hardware.Motherboard;

public class GetAllEndpoint : BaseRequestResponseAccessTokenEndpoint<GetAllRequest, IPagedList<ClientMotherboardInfo>, GetEndpoint>
{
    [HttpPost("Hardware/Motherboard/GetAll")]
    [OpenApiOperation(operationId : "Motherboard.GetAll",
                      summary : "Get all Motherboards",
                      description : "Get all Motherboards")]
    [OpenApiTags("Hardware", "Motherboard", "Client")]
    public override async Task<IPagedList<ClientMotherboardInfo>> HandleAsync(GetAllRequest request, CancellationToken cancellationToken) =>
        await base.HandleAsync(request, cancellationToken);
}
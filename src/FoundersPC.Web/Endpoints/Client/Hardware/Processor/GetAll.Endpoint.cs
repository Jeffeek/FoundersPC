using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Client.Hardware.Processor;
using FoundersPC.Application.Features.Client.Hardware.Processor.Models;
using FoundersPC.SharedKernel.Endpoints;
using FoundersPC.SharedKernel.Pagination;
using FoundersPC.Web.Endpoints.Base;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Client.Hardware.Processor;

public class GetAllEndpoint : BaseRequestResponseAccessTokenEndpoint<GetAllRequest, IPagedList<ClientProcessorInfo>, GetEndpoint>
{
    [HttpPost("Hardware/Processor/GetAll")]
    [OpenApiOperation(operationId : "Processor.GetAll",
                      summary : "Get all Processors",
                      description : "Get all Processors")]
    [OpenApiTags("Hardware", "Processor", "Client")]
    public override async Task<ActionResult<IPagedList<ClientProcessorInfo>>> HandleAsync(GetAllRequest request, CancellationToken cancellationToken) =>
        await base.HandleAsync(request, cancellationToken);
}
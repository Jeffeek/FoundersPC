using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.UserInformation.Models;
using FoundersPC.Application.Features.UserInformation;
using FoundersPC.SharedKernel.Endpoints;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.UserInformation;

public class GetEndpoint : BaseRequestResponseEndpoint<GetRequest, UserInfo, GetEndpoint>
{
    [HttpPost("UserInformation/Get")]
    [OpenApiOperation(operationId : "UserInformation.Get",
                      summary : "Get UserInformation",
                      description : "Get UserInformation")]
    [OpenApiTags("UserInformation")]
    public override async Task<ActionResult<UserInfo>> HandleAsync([FromBody] GetRequest request, CancellationToken cancellationToken) =>
        await Mediator.Send(request, cancellationToken);
}
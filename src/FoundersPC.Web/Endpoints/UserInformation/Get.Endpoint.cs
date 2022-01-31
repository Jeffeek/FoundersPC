using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.UserInformation.Models;
using FoundersPC.Application.Features.UserInformation;
using FoundersPC.SharedKernel.Endpoints;
using FoundersPC.SharedKernel.Exceptions;
using FoundersPC.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.UserInformation;

public class GetEndpoint : BaseRequestResponseEndpoint<GetRequest, UserInfo, GetEndpoint>
{
    private readonly ICurrentUserService _currentUserService;

    public GetEndpoint(ICurrentUserService currentUserService) =>
        _currentUserService = currentUserService;

    [HttpPost("UserInformation/Get")]
    [OpenApiOperation(operationId : "UserInformation.Get",
                      summary : "Get UserInformation",
                      description : "Get UserInformation")]
    [OpenApiTags("UserInformation")]
    public override async Task<ActionResult<UserInfo>> HandleAsync([FromBody] GetRequest request, CancellationToken cancellationToken)
    {
        if (request.Id == null && request.Login == null)
            request.Id = _currentUserService.UserId == 0 ? throw new BadRequestException("Bad request for user info. User not authenticated and credentials are empty") : _currentUserService.UserId;

        return await Mediator.Send(request, cancellationToken);
    }
}
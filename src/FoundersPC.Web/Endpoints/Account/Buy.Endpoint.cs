using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Pricing.Models;
using FoundersPC.Application.Features.UserInformation.Models;
using FoundersPC.SharedKernel.Endpoints;
using Microsoft.AspNetCore.Mvc;

namespace FoundersPC.Web.Endpoints.Account;

public class BuyEndpoint : BaseRequestResponseEndpoint<BuyRequest, AccessTokenInfo, BuyEndpoint>
{
    [HttpPost("Buy")]
    public override async Task<AccessTokenInfo> HandleAsync(BuyRequest request, CancellationToken cancellationToken = new()) =>
        await Mediator.Send(request, cancellationToken);
}
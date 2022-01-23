using FoundersPC.Application.Features.UserInformation.Models;
using FoundersPC.Domain.Enums;
using MediatR;

namespace FoundersPC.Application.Features.Pricing.Models;

public class BuyRequest : IRequest<AccessTokenInfo>
{
    public TokenPackageType PackageType { get; set; }
}
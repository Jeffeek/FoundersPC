using FoundersPC.Application.Features.UserInformation.Models;
using MediatR;

namespace FoundersPC.Application.Features.UserInformation;

public class GetRequest : IRequest<UserInfo>
{
    public int? Id { get; set; }
    public string? Login { get; set; } = default!;
}
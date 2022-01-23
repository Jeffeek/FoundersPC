using FoundersPC.Application.Features.Token.Models;
using MediatR;

namespace FoundersPC.Application.Features.SignUp;

public class SignUpRequest : IRequest<TokenResponse>
{
    public string Email { get; set; } = default!;
    public string Login { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string RepeatPassword { get; set; } = default!;
}
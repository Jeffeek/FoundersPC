using MediatR;

namespace FoundersPC.Application.Features.ForgotPassword;

public class ForgotPasswordRequest : IRequest<ForgotPasswordResponse>
{
    public string Email { get; set; } = default!;
}
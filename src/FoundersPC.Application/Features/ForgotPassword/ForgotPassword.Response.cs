namespace FoundersPC.Application.Features.ForgotPassword;

public class ForgotPasswordResponse
{
    public string Email { get; set; } = default!;
    public string? Message { get; set; }
}
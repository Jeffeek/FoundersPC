namespace FoundersPC.Application.Features.UserInformation.Models;

public class UserViewInfo
{
    public int Id { get; set; }
    public string Login { get; set; } = default!;
    public string Email { get; set; } = default!;
    public bool IsBlocked { get; set; }
    public string Role { get; set; } = default!;
}
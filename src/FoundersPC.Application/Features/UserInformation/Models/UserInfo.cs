using System.Collections.Generic;

namespace FoundersPC.Application.Features.UserInformation.Models;

public class UserInfo
{
    public int Id { get; set; }
    public string Email { get; set; } = default!;
    public string Login { get; set; } = default!;
    public string Role { get; set; } = default!;
    public List<AccessTokenInfo> Tokens { get; set; } = default!;
}
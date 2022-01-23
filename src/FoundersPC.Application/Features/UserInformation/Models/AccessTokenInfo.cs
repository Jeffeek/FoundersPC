using System;

namespace FoundersPC.Application.Features.UserInformation.Models;

public class AccessTokenInfo
{
    public int Id { get; set; }
    public string Token { get; set; } = default!;
    public bool IsActive { get; set; }
    public DateTime ActiveFrom { get; set; }
    public DateTime ActiveTo { get; set; }
}
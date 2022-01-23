using FoundersPC.SharedKernel.Interfaces;

namespace FoundersPC.UI.Admin.Services;

public class CurrentUserService : ICurrentUserService
{
    public int UserId { get; private set; }
    public string Login { get; private set; } = default!;
    public string Role { get; private set; } = default!;

    public void Initialize(int id)
    {
        UserId = id;
    }
}
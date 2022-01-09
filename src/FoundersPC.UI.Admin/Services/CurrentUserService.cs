using FoundersPC.SharedKernel.Interfaces;

namespace FoundersPC.UI.Admin.Services;

public class CurrentUserService : ICurrentUserService
{
    public int UserId { get; private set; }

    public void Initialize(int id) => UserId = id;
}
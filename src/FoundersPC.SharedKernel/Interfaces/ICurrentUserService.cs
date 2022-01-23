using System.Threading.Tasks;

namespace FoundersPC.SharedKernel.Interfaces;

public interface ICurrentUserService
{
    int UserId { get; }
    string Login { get; }
    string Role { get; }
    void Initialize(int id) { }
    void Initialize() { }
    Task InitializeAsync(int id) => Task.CompletedTask;
    Task InitializeAsync() => Task.CompletedTask;
}
namespace FoundersPC.SharedKernel.Interfaces;

public interface ICurrentUserService
{
    int UserId { get; }
    void Initialize(int id) { }
}
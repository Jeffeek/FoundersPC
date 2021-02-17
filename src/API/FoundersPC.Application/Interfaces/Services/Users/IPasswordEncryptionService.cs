namespace FoundersPC.Application.Interfaces.Services.Users
{
    public interface IPasswordEncryptionService
    {
        string Encrypt(string password);
    }
}
namespace FoundersPC.Identity.Application.Interfaces.Services.Encryption_Services
{
    public interface IPasswordEncryptorService
    {
        string EncryptPassword(string rawPassword);

        string GeneratePassword(int length = 6);
    }
}
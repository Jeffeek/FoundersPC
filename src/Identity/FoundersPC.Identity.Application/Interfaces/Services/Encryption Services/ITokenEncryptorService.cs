namespace FoundersPC.Identity.Application.Interfaces.Services.Encryption_Services
{
    public interface ITokenEncryptorService
    {
        string Encrypt(string rawToken, string key);

        string CreateRawToken();
    }
}
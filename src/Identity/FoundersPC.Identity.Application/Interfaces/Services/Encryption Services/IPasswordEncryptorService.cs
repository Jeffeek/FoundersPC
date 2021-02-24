namespace FoundersPC.Identity.Application.Interfaces.Services.Encryption_Services
{
    public interface IPasswordEncryptorService
    {
        public string EncryptPassword(string rawPassword);
    }
}
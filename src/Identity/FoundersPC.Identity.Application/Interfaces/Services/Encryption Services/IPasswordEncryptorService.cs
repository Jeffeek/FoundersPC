namespace FoundersPC.Identity.Application.Interfaces.Services
{
    public interface IPasswordEncryptorService
    {
        public string EncryptPassword(string rawPassword);
    }
}

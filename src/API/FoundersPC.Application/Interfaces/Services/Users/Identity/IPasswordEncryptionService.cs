namespace FoundersPC.Application.Interfaces.Services.Users.Identity
{
	public interface IPasswordEncryptionService
	{
		string Encrypt(string password);
	}
}
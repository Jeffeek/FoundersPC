using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoundersPC.Application.Interfaces.Services.Users
{
	public interface IEmailSenderService
	{
		Task<bool> SendToAsync(int id, string content);

		Task<int> SendManyAsync(IEnumerable<int> ids, string content);
	}
}
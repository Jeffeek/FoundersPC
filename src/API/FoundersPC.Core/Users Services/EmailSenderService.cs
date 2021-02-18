using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Services.Users;

namespace FoundersPC.Services.Users_Services
{
	public class EmailSenderService : IEmailSenderService
	{
		#region Implementation of IEmailSenderService

		/// <inheritdoc />
		public Task<bool> SendToAsync(int id, string content) => throw new System.NotImplementedException();

		/// <inheritdoc />
		public Task<int> SendManyAsync(IEnumerable<int> ids, string content) => throw new System.NotImplementedException();

		#endregion
	}
}
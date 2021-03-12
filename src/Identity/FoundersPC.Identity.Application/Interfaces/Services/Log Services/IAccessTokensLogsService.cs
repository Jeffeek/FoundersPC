#region Using namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Identity.Domain.Entities.Logs;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Services.Log_Services
{
	public interface IAccessTokensLogsService
	{
		Task<IEnumerable<AccessTokenLog>> GetAllAsync();

		Task<AccessTokenLog> GetByIdAsync(int id);

		Task<IEnumerable<AccessTokenLog>> GetUsagesBetweenAsync(DateTime start, DateTime finish);

		Task<IEnumerable<AccessTokenLog>> GetUsagesInAsync(DateTime date);

		Task<bool> LogAsync(int tokenId);

		Task<bool> LogAsync(string token);
	}
}
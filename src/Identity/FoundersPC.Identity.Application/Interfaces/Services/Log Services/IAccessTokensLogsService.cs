#region Using namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Identity.Dto;
using FoundersPC.ServicesShared;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Services.Log_Services
{
    // TODO ISP maybe..
    public interface IAccessTokensLogsService : IPaginateableService<AccessTokenLogReadDto>
    {
        Task<IEnumerable<AccessTokenLogReadDto>> GetAllTokensLogsAsync();

        Task<AccessTokenLogReadDto> GetTokenLogByIdAsync(int id);

        Task<IEnumerable<AccessTokenLogReadDto>> GetUsagesBetweenAsync(DateTime start, DateTime finish);

        Task<IEnumerable<AccessTokenLogReadDto>> GetUsagesInAsync(DateTime date);

        Task<AccessTokenLogReadDto> GetLastTokenUsageAsync(int apiAccessTokenId);

        Task<AccessTokenLogReadDto> GetLastTokenUsageAsync(string apiAccessToken);

        Task<IEnumerable<AccessTokenLogReadDto>> GetTokenLogsAsync(int tokenId);

        Task<IEnumerable<AccessTokenLogReadDto>> GetTokenLogsAsync(string token);

        Task<IEnumerable<AccessTokenLogReadDto>> GetUserTokenUsagesByUserIdAsync(int userId);

        Task<IEnumerable<AccessTokenLogReadDto>> GetUserTokenUsagesByUserEmailAsync(string userEmail);

        Task<bool> LogAsync(int tokenId);

        Task<bool> LogAsync(string token);
    }
}
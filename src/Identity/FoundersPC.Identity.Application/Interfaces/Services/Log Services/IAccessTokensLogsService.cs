#region Using namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Identity.Dto;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Services.Log_Services
{
    public interface IAccessTokensLogsService
    {
        Task<IEnumerable<AccessTokenLogReadDto>> GetAllAsync();

        Task<AccessTokenLogReadDto> GetByIdAsync(int id);

        Task<IEnumerable<AccessTokenLogReadDto>> GetUsagesBetweenAsync(DateTime start, DateTime finish);

        Task<IEnumerable<AccessTokenLogReadDto>> GetUsagesInAsync(DateTime date);

        Task<AccessTokenLogReadDto> GetLastTokenUsageAsync(int apiAccessTokenId);

        Task<AccessTokenLogReadDto> GetLastTokenUsageAsync(string apiAccessToken);

        Task<bool> LogAsync(int tokenId);

        Task<bool> LogAsync(string token);
    }
}
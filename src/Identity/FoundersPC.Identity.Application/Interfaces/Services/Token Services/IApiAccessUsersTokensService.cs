﻿#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Services.Token_Services
{
    public interface IApiAccessUsersTokensService
    {
        Task<bool> IsTokenBlockedAsync(string token);

        Task<bool> IsTokenActiveAsync(string token);

        Task<bool> CanMakeRequestAsync(string token);

        Task<bool> BlockAsync(string token);

        Task<bool> IsTokenBlockedAsync(int id);

        Task<bool> IsTokenActiveAsync(int id);

        Task<bool> CanMakeRequestAsync(int tokenId);

        Task<bool> BlockAsync(int id);

        Task<IEnumerable<ApiAccessUserTokenReadDto>> GetUserTokens(int userId);

        Task<IEnumerable<ApiAccessUserTokenReadDto>> GetUserTokens(string userEmail);
    }
}
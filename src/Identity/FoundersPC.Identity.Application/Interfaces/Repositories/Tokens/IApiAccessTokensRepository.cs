﻿using FoundersPC.ApplicationShared.Repository;
using FoundersPC.Identity.Domain.Entities;

namespace FoundersPC.Identity.Application.Interfaces.Repositories
{
    public interface IApiAccessTokensRepository : IRepositoryAsync<ApiAccessToken> { }
}

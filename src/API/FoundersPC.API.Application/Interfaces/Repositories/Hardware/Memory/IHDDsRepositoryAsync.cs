﻿#region Using namespaces

using FoundersPC.API.Domain.Entities.Hardware.Memory;
using FoundersPC.ApplicationShared.Repository;

#endregion

namespace FoundersPC.API.Application.Interfaces.Repositories.Hardware.Memory
{
    public interface IHDDsRepositoryAsync : IRepositoryAsync<HDD> { }
}
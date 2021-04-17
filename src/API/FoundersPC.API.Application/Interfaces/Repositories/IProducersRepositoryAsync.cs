﻿#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Domain.Entities;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.API.Application.Interfaces.Repositories
{
    public interface IProducersRepositoryAsync : IRepositoryAsync<ProducerEntity>,
                                                 IPaginateableRepository<ProducerEntity>
    {
        Task<IEnumerable<ProducerEntity>> GetAllWithHardwareAsync();
    }
}
﻿#region Using namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories;
using FoundersPC.API.Domain.Entities;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace HardwareApi.Tests.MockAbstractions.Repositories
{
    public class MockProducersRepository : GenericRepositoryAsync<ProducerEntity>, IProducersRepositoryAsync
    {
        /// <inheritdoc/>
        public MockProducersRepository(DbContext context) : base(context) { }

        #region Implementation of IProducersRepositoryAsync

        /// <inheritdoc/>
        public Task<IEnumerable<ProducerEntity>> GetAllWithHardwareAsync() => throw new NotImplementedException();

        #endregion

        #region Implementation of IPaginateableRepository<ProducerEntity>

        /// <inheritdoc/>
        public Task<IEnumerable<ProducerEntity>> GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) => throw new NotImplementedException();

        #endregion
    }
}
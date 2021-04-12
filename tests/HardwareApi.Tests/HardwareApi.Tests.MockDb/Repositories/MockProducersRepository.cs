#region Using namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Repositories.Hardware;
using FoundersPC.API.Domain.Entities.Hardware;
using FoundersPC.RepositoryShared.Repository;
using Microsoft.EntityFrameworkCore;

#endregion

namespace HardwareApi.Tests.MockAbstractions.Repositories
{
    public class MockProducersRepository : GenericRepositoryAsync<Producer>, IProducersRepositoryAsync
    {
        /// <inheritdoc/>
        public MockProducersRepository(DbContext context) : base(context) { }

        #region Implementation of IProducersRepositoryAsync

        /// <inheritdoc/>
        public Task<IEnumerable<Producer>> GetAllWithHardwareAsync() => throw new NotImplementedException();

        #endregion

        #region Implementation of IPaginateableRepository<Producer>

        /// <inheritdoc/>
        public Task<IEnumerable<Producer>> GetPaginateableAsync(int pageNumber = 1, int pageSize = 10) =>
            throw new NotImplementedException();

        #endregion
    }
}
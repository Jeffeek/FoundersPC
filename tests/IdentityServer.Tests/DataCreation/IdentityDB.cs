#region Using namespaces

using FoundersPC.API.Application.Services;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

#endregion

namespace IdentityServer.Tests.DataCreation
{
    public static class IdentityDB
    {
        public static ApplicationDbContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                          .UseInMemoryDatabase("InMem")
                          .Options;

            return new ApplicationDbContext(options, new CurrentUserServiceMock(), new CurrentDateTimeService());
        }
    }

    public class CurrentUserServiceMock : ICurrentUserService
    {
        #region Implementation of ICurrentUserService

        /// <inheritdoc />
        public int UserId => 0;

        #endregion
    }
}
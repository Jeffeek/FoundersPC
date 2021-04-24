#region Using namespaces

using FoundersPC.Identity.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

#endregion

namespace IdentityServer.Tests.MockDb
{
    public static class DB
    {
        public static FoundersPCUsersContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<FoundersPCUsersContext>()
                          .UseInMemoryDatabase("InMem")
                          .Options;

            return new FoundersPCUsersContext(options);
        }
    }
}
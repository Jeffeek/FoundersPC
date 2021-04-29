#region Using namespaces

using FoundersPC.API.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

#endregion

namespace HardwareApi.Tests.MockDb
{
    public static class DB
    {
        public static FoundersPCHardwareContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<FoundersPCHardwareContext>()
                          .UseInMemoryDatabase("InMem")
                          .Options;

            return new FoundersPCHardwareContext(options);
        }
    }
}
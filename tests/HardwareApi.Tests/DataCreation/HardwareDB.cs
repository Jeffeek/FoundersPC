#region Using namespaces

using FoundersPC.API.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

#endregion

namespace HardwareApi.Tests.DataCreation
{
    public static class HardwareDB
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
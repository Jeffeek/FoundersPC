#region Using namespaces

using System;
using FoundersPC.API.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

#endregion

namespace HardwareApi.Tests.MockAbstractions.Contexts
{
    public class MockHardwareDbContext : IDisposable
    {
        private FoundersPCHardwareContext _context;

        public MockHardwareDbContext() =>
            Options = new DbContextOptionsBuilder<FoundersPCHardwareContext>().UseInMemoryDatabase("TestHardwareApiDb")
                                                                              .Options;

        public DbContextOptions<FoundersPCHardwareContext> Options { get; }

        public FoundersPCHardwareContext Context => _context ??= new FoundersPCHardwareContext(Options);

        #region IDisposable

        /// <inheritdoc/>
        public void Dispose() => _context?.Dispose();

        #endregion
    }
}
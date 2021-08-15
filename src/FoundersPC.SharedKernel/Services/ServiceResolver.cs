using System;
using Microsoft.Extensions.DependencyInjection;

namespace FoundersPC.SharedKernel.Services
{
    public static class ServiceResolver
    {
        private static IServiceProvider _provider;

        public static void Configure(IServiceProvider provider)
        {
            if (_provider != null)
                return;

            _provider = provider;
        }

        public static T Resolve<T>() where T : class => (T)_provider.GetRequiredService(typeof(T));
    }
}
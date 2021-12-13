#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#endregion

namespace FoundersPC.SharedKernel.Extensions;

public static class ReflectionExtensions
{
    public static IEnumerable<Assembly> GetAllAssemblies() =>
        AppDomain.CurrentDomain
                 .GetAssemblies()
                 .Where(x => x.FullName != null && x.FullName.Contains("FoundersPC"));
}
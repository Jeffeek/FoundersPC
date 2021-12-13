#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace FoundersPC.SharedKernel.Extensions;

public static class EnumerableExtensions
{
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (action == null) throw new ArgumentNullException(nameof(action));

        foreach (var item in source)
            action(item);
    }

    public static bool IsEmpty<T>(this IEnumerable<T>? source)
    {
        if (source == null) return true;

        return !source.Any();
    }
}
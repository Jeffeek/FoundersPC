using System;

namespace FoundersPC.SharedKernel.Extensions;

public static class StringExtensions
{
    public static string RemoveSpaces(this string source)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));

        if (source.Length == 0)
            return source;

        return source.IndexOf(' ') != -1 ? source.Replace(" ", String.Empty) : source;
    }
}
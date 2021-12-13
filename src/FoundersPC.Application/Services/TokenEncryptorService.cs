#region Using namespaces

using System;
using System.Security.Cryptography;
using System.Text;

#endregion

namespace FoundersPC.Application.Services;

public class TokenEncryptorService
{
    /// <exception cref="T:System.ArgumentNullException"><paramref name="s"/> is <see langword="null"/>.</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///     A fallback occurred (for more information, see Character Encoding in .NET)
    ///     -and-
    ///     <see cref="P:System.Text.Encoding.EncoderFallback"/> is set to <see cref="T:System.Text.EncoderExceptionFallback"/>
    ///     .
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///     On the .NET Framework 4.6.1 and earlier versions only:
    ///     The algorithm was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS
    ///     compatible.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="capacity"/> is less than zero.</exception>
    /// <exception cref="T:System.FormatException">
    ///     <paramref name="format"/> includes an unsupported specifier. Supported
    ///     format specifiers are listed in the Remarks section.
    /// </exception>
    public string CreateToken()
    {
        var rawToken = CreateRawToken();
        var tokenBytes = Encoding.Unicode.GetBytes(rawToken);

        using var hash = SHA256.Create();
        var hashedInputBytes = hash.ComputeHash(tokenBytes);

        var hashedInputStringBuilder = new StringBuilder(64);

        foreach (var tokenByte in hashedInputBytes)
            hashedInputStringBuilder.Append(tokenByte.ToString("X2"));

        return hashedInputStringBuilder.ToString();
    }

    private string CreateRawToken()
    {
        var guid = Guid.NewGuid();

        var rawToken = guid.ToString()
                           .Replace("-", String.Empty)
                           .ToUpperInvariant();

        return rawToken;
    }
}
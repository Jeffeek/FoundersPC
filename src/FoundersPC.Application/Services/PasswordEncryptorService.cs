#region Using namespaces

using System;
using BCrypt.Net;
using FoundersPC.Application.Settings;
using Microsoft.Extensions.Options;

#endregion

namespace FoundersPC.Application.Services;

public class PasswordEncryptorService
{
    private readonly PasswordSettings _passwordSettings;

    public PasswordEncryptorService(IOptions<PasswordSettings> passwordSettings) => _passwordSettings = passwordSettings.Value;

    /// <exception cref="T:System.ArgumentNullException"><paramref name="rawPassword"/> is <see langword="null"/></exception>
    /// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///     A fallback occurred (for more information, see Character Encoding in .NET)
    ///     -and-
    ///     <see cref="P:System.Text.Encoding.EncoderFallback"/> is set to <see cref="T:System.Text.EncoderExceptionFallback"/>
    ///     .
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///     The algorithm was used with Federal Information
    ///     Processing Standards (FIPS) mode enabled, but is not FIPS compatible.
    /// </exception>
    public string EncryptPassword(string rawPassword) =>
        BCrypt.Net.BCrypt.HashString(rawPassword + _passwordSettings.Salt,
                                     _passwordSettings.WorkFactor,
                                     SaltRevision.Revision2X);

    public bool VerifyPassword(string rawPassword, string hash) => BCrypt.Net.BCrypt.Verify(rawPassword + _passwordSettings.Salt, hash);

    /// <exception cref="T:System.ArgumentOutOfRangeException">Condition.</exception>
    public static string GeneratePassword(int length = 6)
    {
        if (length is < 6 or > 30)
            throw new ArgumentOutOfRangeException(nameof(length));

        var guid = Guid.NewGuid();

        var guidPass = guid.ToString()
                           .Replace("-", String.Empty);

        return guidPass[..length];
    }
}
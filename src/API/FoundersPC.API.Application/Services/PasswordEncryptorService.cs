#region Using namespaces

using System;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;
using FoundersPC.API.Application.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

#endregion

namespace FoundersPC.API.Application.Services
{
    public class PasswordEncryptorService
    {
        private readonly PasswordSettings _passwordSettings;

        public PasswordEncryptorService(IOptions<PasswordSettings> passwordSettings) =>
            _passwordSettings = passwordSettings.Value;

        /// <exception cref="T:System.ArgumentNullException"><paramref name="rawPassword"/> is <see langword="null"/></exception>
        /// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
        /// <exception cref="T:System.Text.EncoderFallbackException">
        ///     A fallback occurred (for more information, see Character Encoding in .NET)
        ///     -and-
        ///     <see cref="P:System.Text.Encoding.EncoderFallback"/> is set to <see cref="T:System.Text.EncoderExceptionFallback"/>
        ///     .
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="capacity"/> is less than zero.</exception>
        /// <exception cref="T:System.FormatException">
        ///     <paramref name="format"/> includes an unsupported specifier. Supported
        ///     format specifiers are listed in the Remarks section.
        /// </exception>
        /// <exception cref="T:System.Reflection.TargetInvocationException">
        ///     The algorithm was used with Federal Information
        ///     Processing Standards (FIPS) mode enabled, but is not FIPS compatible.
        /// </exception>
        public string EncryptPassword(string rawPassword) =>
            BCrypt.Net.BCrypt.HashString(rawPassword + _passwordSettings.Salt,
                                         _passwordSettings.WorkFactor,
                                         SaltRevision.Revision2X);

        public bool VerifyPassword(string rawPassword, string hash) =>
            BCrypt.Net.BCrypt.Verify(rawPassword + _passwordSettings.Salt, hash);

        /// <exception cref="T:System.ArgumentOutOfRangeException">Condition.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="oldValue"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="oldValue"/> is the empty string ("").</exception>
        public string GeneratePassword(int length = 6)
        {
            if (length < 6
                || length > 30)
                throw new ArgumentOutOfRangeException(nameof(length));

            var guid = Guid.NewGuid();

            var guidPass = guid.ToString()
                               .Replace("-", String.Empty);

            return guidPass.Substring(0, length);
        }
    }
}
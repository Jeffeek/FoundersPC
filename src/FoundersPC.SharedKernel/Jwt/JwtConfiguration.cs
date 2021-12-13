#region Using namespaces

using System.Text;
using Microsoft.IdentityModel.Tokens;

#endregion

namespace FoundersPC.SharedKernel.Jwt;

public class JwtConfiguration
{
    #region Docs

    /// <exception cref="T:System.NotSupportedException"
    ///            accessor="get">
    ///     Inner JWT configuration was null.
    /// </exception>

    #endregion

    public string Key { get; set; }

    public string Issuer { get; set; }

    public string Audience { get; set; }

    public int MinutesToExpire { get; set; }

    #region Docs

    /// <exception cref="T:System.ArgumentNullException"><paramref name="Key"/> is <see langword="null"/>.</exception>
    /// <exception cref="T:System.Text.EncoderFallbackException">
    ///     A fallback occurred (for more information, see Character Encoding in .NET)
    ///     -and-
    ///     <see cref="P:System.Text.Encoding.EncoderFallback"/> is set to <see cref="T:System.Text.EncoderExceptionFallback"/>
    ///     .
    /// </exception>

    #endregion

    public SymmetricSecurityKey GetSymmetricSecurityKey() => new(Encoding.ASCII.GetBytes(Key));

    #region Docs

    /// <exception cref="T:System.OverflowException">
    ///     <paramref name="configuration[JwtSettings:HoursToExpire]"/> represents a
    ///     number less than <see cref="F:System.Int32.MinValue"/> or greater than <see cref="F:System.Int32.MaxValue"/>.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///     <paramref name="configuration[JwtSettings:HoursToExpire]"/> is
    ///     <see langword="null"/>.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///     <paramref name="configuration[JwtSettings:HoursToExpire]"/> is not in the
    ///     correct format.
    /// </exception>
    /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">JwtSettings:Issuer</exception>
    /// <exception cref="T:System.NotSupportedException">Configuration exception.</exception>

    #endregion
}
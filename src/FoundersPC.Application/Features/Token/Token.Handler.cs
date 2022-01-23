#region Using namespaces

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Token.Models;
using FoundersPC.Domain.Entities.Identity.Users;
using FoundersPC.SharedKernel;
using FoundersPC.SharedKernel.Exceptions;
using FoundersPC.SharedKernel.Interfaces;
using FoundersPC.SharedKernel.Jwt;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

#endregion

namespace FoundersPC.Application.Features.Token;

public class TokenHandler : IRequestHandler<TokenRequest, TokenResponse>
{
    private readonly JwtConfiguration _authOptions;
    private readonly IDateTimeService _dateTime;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public TokenHandler(UserManager<ApplicationUser> userManager,
                        SignInManager<ApplicationUser> signInManager,
                        IDateTimeService dateTime,
                        IOptions<JwtConfiguration> jwtConfiguration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _dateTime = dateTime;
        _authOptions = jwtConfiguration.Value;
    }

    public async Task<TokenResponse> Handle(TokenRequest request, CancellationToken cancellationToken) =>
        request.GrantType switch
        {
            GrantTypes.Password     => await LoginByPasswordAsync(request),
            GrantTypes.RefreshToken => await RefreshTokenAsync(request),
            _                       => throw new BadRequestException("invalid_grant", "Unsupported grant type")
        };

    private async Task<TokenResponse> LoginByPasswordAsync(TokenRequest request)
    {
        var user = await _userManager.FindByIdAsync(request.Login);

        if (user == null)
            throw new BadRequestException("invalid_grant", "Invalid username or password");

        if (!await _signInManager.CanSignInAsync(user)
            || user.IsBlocked)
            throw new BadRequestException("invalid_grant", "The specified user cannot sign in.");

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
            throw new BadRequestException("invalid_grant", "Invalid username or password");

        // Create the principal
        var principal = await _signInManager.CreateUserPrincipalAsync(user);

        return GenerateToken(principal, user);
    }

    private async Task<TokenResponse> RefreshTokenAsync(TokenRequest request)
    {
        if (String.IsNullOrEmpty(request.RefreshToken))
            throw new BadRequestException("invalid_grant", "Bad refresh_token format");

        var tokenValidationParameters = new TokenValidationParameters
                                        {
                                            ValidateAudience = true,
                                            ValidAudience = "Refresh",
                                            ValidateIssuer = true,
                                            ValidIssuer = _authOptions.Issuer,
                                            ValidateIssuerSigningKey = true,
                                            IssuerSigningKey = _authOptions.GetSymmetricSecurityKey(),
                                            ValidateLifetime = true
                                        };

        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var principal = tokenHandler.ValidateToken(request.RefreshToken,
                                                       tokenValidationParameters,
                                                       out var securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken
                || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                                                       StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            var user = await _userManager.FindByIdAsync(principal.Identity?.Name);

            if (user == null)
                throw new BadRequestException("invalid_grant", "Invalid username or password");

            if (user.IsBlocked)
                throw new BadRequestException("invalid_grant", "User is blocked");

            return GenerateToken(principal, user);
        }
        catch (Exception ste) when (ste is SecurityTokenExpiredException)
        {
            throw new BadRequestException("invalid_grant", "The token has expired. Please login.");
        }
        catch (Exception ste)
        {
            throw new BadRequestException("invalid_grant", ste.Message);
        }
    }

    private TokenResponse GenerateToken(ClaimsPrincipal principal, ApplicationUser user)
    {
        var now = _dateTime.Now;
        var expires = now.Add(TimeSpan.FromMinutes(_authOptions.MinutesToExpire));

        var jwt = new JwtSecurityToken(_authOptions.Issuer,
                                       _authOptions.Audience,
                                       notBefore : now,
                                       claims : principal.Claims,
                                       expires : expires,
                                       signingCredentials : new(_authOptions.GetSymmetricSecurityKey(),
                                                                SecurityAlgorithms.HmacSha256));

        var jwtAccessToken = new JwtSecurityTokenHandler().WriteToken(jwt);

        var expiresRefresh = now.Add(TimeSpan.FromMinutes(_authOptions.MinutesToExpire));

        var refreshJwt = new JwtSecurityToken(_authOptions.Issuer,
                                              "Refresh",
                                              notBefore : now,
                                              claims : principal.Claims,
                                              expires : expiresRefresh,
                                              signingCredentials : new(_authOptions.GetSymmetricSecurityKey(),
                                                                       SecurityAlgorithms.HmacSha256));

        var jwtRefreshToken = new JwtSecurityTokenHandler().WriteToken(refreshJwt);

        return new()
               {
                   Expires = expires,
                   Issued = _authOptions.Issuer,
                   AccessToken = jwtAccessToken,
                   Email = user.Email,
                   RefreshToken = jwtRefreshToken,
                   TokenLifetimeInSec = _authOptions.MinutesToExpire * 60,
                   Role = user.ApplicationRole.Name,
                   TokenType = JwtBearerDefaults.AuthenticationScheme,
                   Login = user.Login
               };
    }
}
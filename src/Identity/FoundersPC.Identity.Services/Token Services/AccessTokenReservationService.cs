#region Using namespaces

using System;
using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Services.Mail_service;
using FoundersPC.Identity.Application.Interfaces.Services.Token_Services;
using FoundersPC.Identity.Domain.Entities.Tokens;
using FoundersPC.Identity.Domain.Entities.Users;
using FoundersPC.Identity.Dto;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
using FoundersPC.Identity.Services.Encryption_Services;
using FoundersPC.RequestResponseShared.IdentityServer.Request.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Identity.Services.Token_Services
{
    public class AccessTokenReservationService : IAccessTokensReservationService
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<AccessTokenReservationService> _logger;
        private readonly TokenEncryptorService _tokenEncryptorService;
        private readonly IUnitOfWorkUsersIdentity _unitOfWork;

        public AccessTokenReservationService(IUnitOfWorkUsersIdentity unitOfWork,
                                             TokenEncryptorService tokenEncryptorService,
                                             IEmailService emailService,
                                             ILogger<AccessTokenReservationService> logger)
        {
            _unitOfWork = unitOfWork;
            _tokenEncryptorService = tokenEncryptorService;
            _emailService = emailService;
            _logger = logger;
        }

        /// <exception cref="T:System.NotSupportedException">
        ///     Reserve new tokenEntity: user with <paramref name="userEmail"/> not
        ///     found.
        /// </exception>
        public async Task<AccessUserTokenReadDto> ReserveNewTokenAsync(string userEmail, TokenType type)
        {
            var user = await _unitOfWork.UsersRepository.GetUserByEmailAsync(userEmail);

            if (user is null)
                throw new
                    NotSupportedException($"{nameof(AccessTokenReservationService)}: Reserve new tokenEntity: user with email = {userEmail} not found");

            return await ReserveNewTokenAsync(user, type);
        }

        /// <exception cref="T:System.NotSupportedException">
        ///     Reserve new tokenEntity: user with <paramref name="userId"/> not
        ///     found.
        /// </exception>
        public async Task<AccessUserTokenReadDto> ReserveNewTokenAsync(int userId, TokenType type)
        {
            var user = await _unitOfWork.UsersRepository.GetByIdAsync(userId);

            if (user is null)
                throw new
                    NotSupportedException($"{nameof(AccessTokenReservationService)}: Reserve new tokenEntity: user with id = {userId} not found");

            return await ReserveNewTokenAsync(user, type);
        }

        private async Task<AccessUserTokenReadDto> ReserveNewTokenAsync(UserEntity user, TokenType type)
        {
            var tokenStartEvaluation = DateTime.Now;
            var tokenLifetime = GetTheExpirationDate(type);
            var newHashedToken = _tokenEncryptorService.CreateToken();

            var newToken = new AccessTokenEntity
                           {
                               ExpirationDate = tokenLifetime,
                               IsBlocked = false,
                               StartEvaluationDate = tokenStartEvaluation,
                               User = user,
                               UserId = user.Id,
                               HashedToken = newHashedToken
                           };

            var entity = await _unitOfWork.AccessTokensRepository.AddAsync(newToken);

            var saveChangesResult = await _unitOfWork.SaveChangesAsync() > 0;

            if (!saveChangesResult)
            {
                _logger.LogWarning($"Database not saved changes when tried to reserve new access tokenEntity. UserId = {user.Id}");

                throw new DbUpdateException($"Database not saved changes when tried to reserve new access tokenEntity. UserId = {user.Id}");
            }

            await _emailService.SendAPIAccessTokenAsync(user.Email, newHashedToken);

            return new AccessUserTokenReadDto
                   {
                       ExpirationDate = tokenLifetime,
                       HashedToken = newHashedToken,
                       Id = entity.Id,
                       IsBlocked = false,
                       StartEvaluationDate = tokenStartEvaluation,
                       UserId = user.Id
                   };
        }

        // this method calculates the expiration date, it bases on TokenType.
        // Numbers are months.
        // Why ENDLESS is just 1200 month? I don't know, it's a 100 years probably O_O
        private static DateTime GetTheExpirationDate(TokenType type)
        {
            var now = DateTime.Now;

            var expirationDate = now.AddMonths(type switch
                                               {
                                                   TokenType.Low         => 2,
                                                   TokenType.Medium      => 6,
                                                   TokenType.High        => 12,
                                                   TokenType.Ultra       => 60,
                                                   TokenType.Unstoppable => 1200,
                                                   _                     => throw new ArgumentOutOfRangeException(nameof(type), type, null)
                                               });

            return expirationDate;
        }
    }
}
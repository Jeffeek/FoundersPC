#region Using namespaces

using System;
using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Services.Token_Services;
using FoundersPC.Identity.Domain.Entities.Tokens;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
using FoundersPC.Identity.Services.Encryption_Services;
using FoundersPC.RequestResponseShared.Request.Tokens;
using FoundersPC.WebIdentityShared;
using Microsoft.IdentityModel.Tokens;

#endregion

namespace FoundersPC.Identity.Services.Token_Services
{
    // todo: inject
    // todo: add mail service to throw token into email
    public class ApiAccessTokenReservationService : IApiAccessTokensReservationService
    {
        private readonly IUnitOfWorkUsersIdentity _unitOfWork;
        private readonly TokenEncryptorService _tokenEncryptorService;

        public ApiAccessTokenReservationService(IUnitOfWorkUsersIdentity unitOfWork,
                                                TokenEncryptorService tokenEncryptorService)
        {
            _unitOfWork = unitOfWork;
            _tokenEncryptorService = tokenEncryptorService;
        }

        public async Task<ApplicationAccessToken> ReserveNewTokenAsync(string userEmail, TokenType type)
        {
            var tokenStartEvaluation = DateTime.Now;
            var tokenLifetime = GetTheExpirationDate(type);

            var user = await _unitOfWork.UsersRepository.GetByAsync(x => x.Email == userEmail);

            if (user is null)
                throw new
                    NotSupportedException($"{nameof(ApiAccessTokenReservationService)}: Reserve new token: user with email = {userEmail} not found");

            var newHashedToken = _tokenEncryptorService.CreateToken();

            var newToken = new ApiAccessUserToken()
                           {
                               ExpirationDate = tokenLifetime,
                               IsBlocked = false,
                               StartEvaluationDate = tokenStartEvaluation,
                               User = user,
                               UserId = user.Id,
                               HashedToken = newHashedToken
                           };

            var entity = await _unitOfWork.ApiAccessUsersTokensRepository.AddAsync(newToken);

            var saveChangesResult = await _unitOfWork.SaveChangesAsync() > 0;

            if (saveChangesResult)
                return new ApplicationAccessToken()
                       {
                           ExpirationDate = tokenLifetime,
                           HashedToken = newHashedToken,
                           Id = entity.Id,
                           IsBlocked = false,
                           StartEvaluationDate = tokenStartEvaluation,
                           UserId = user.Id
                       };

            // todo: maybe throw exception
            return null;
        }

        // this method calculates the expiration date, it bases on TokenType.
        // Numbers are months.
        // Why ENDLESS is just 1200 month? I don't know, it's a 100 years probably o_O
        private DateTime GetTheExpirationDate(TokenType type)
        {
            var now = DateTime.Now;
            var expirationDate = now.AddMonths(type switch
                                               {
                                                   TokenType.Low => 2,
                                                   TokenType.Medium => 6,
                                                   TokenType.High => 12,
                                                   TokenType.Ultra => 60,
                                                   TokenType.Unstoppable => 1200,
                                                   _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
                                               });

            return expirationDate;
        }
    }
}
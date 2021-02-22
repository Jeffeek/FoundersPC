using System;
using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Services.Encryption_Services;
using FoundersPC.Identity.Application.Interfaces.Services.Token_Services;
using FoundersPC.Identity.Domain.Entities.Tokens;
using FoundersPC.Identity.Infrastructure.UnitOfWork;

namespace FoundersPC.Identity.Services.Token_Services
{
    [Obsolete]
    public class ApiAccessTokensService : IApiAccessTokensService
    {
        private readonly IUnitOfWorkUsersIdentity _unitOfWork;
        private readonly ITokenEncryptorService _tokenEncryptorService;

        public ApiAccessTokensService(IUnitOfWorkUsersIdentity unitOfWork,
                                      ITokenEncryptorService tokenEncryptorService)
        {
            _unitOfWork = unitOfWork;
            _tokenEncryptorService = tokenEncryptorService;
        }

        public async Task Generate(int countOfGenerated)
        {
            if (countOfGenerated <= 0) return;

            while (countOfGenerated > 0)
            {
                var newToken = _tokenEncryptorService.CreateRawToken();
                await _unitOfWork.ApiAccessTokensRepository.AddAsync(new ApiAccessToken()
                                                                     {
                                                                         HashedToken = newToken
                                                                     });
                countOfGenerated--;
            }
        }
    }
}

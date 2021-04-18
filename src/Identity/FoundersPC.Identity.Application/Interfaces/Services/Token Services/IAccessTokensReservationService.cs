#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.Identity.Dto;
using FoundersPC.RequestResponseShared.IdentityServer.Request.Tokens;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Services.Token_Services
{
    public interface IAccessTokensReservationService
    {
        Task<AccessTokenReadDto> ReserveNewTokenAsync(string userEmail, TokenType type);

        Task<AccessTokenReadDto> ReserveNewTokenAsync(int userId, TokenType type);
    }
}
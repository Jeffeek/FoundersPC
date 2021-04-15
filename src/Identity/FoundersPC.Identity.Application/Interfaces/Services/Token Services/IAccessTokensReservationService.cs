#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.Identity.Dto;
using FoundersPC.RequestResponseShared.Request.Tokens;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Services.Token_Services
{
    public interface IAccessTokensReservationService
    {
        Task<AccessUserTokenReadDto> ReserveNewTokenAsync(string userEmail, TokenType type);

        Task<AccessUserTokenReadDto> ReserveNewTokenAsync(int userId, TokenType type);
    }
}
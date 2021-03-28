#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.RequestResponseShared.Request.Tokens;
using FoundersPC.WebIdentityShared;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Services.Token_Services
{
    // todo: add variation with user id
    public interface IApiAccessTokensReservationService
    {
        Task<ApplicationAccessToken> ReserveNewTokenAsync(string userEmail, TokenType type);
    }
}
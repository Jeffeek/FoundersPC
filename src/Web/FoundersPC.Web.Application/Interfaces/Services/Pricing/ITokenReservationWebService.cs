#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.RequestResponseShared.IdentityServer.Request.Tokens;
using FoundersPC.RequestResponseShared.IdentityServer.Response.Tokens;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.Pricing
{
    public interface ITokenReservationWebService
    {
        Task<BuyNewTokenResponse> ReserveNewTokenAsync(TokenType type, string userEmail, string userJwtToken);
    }
}
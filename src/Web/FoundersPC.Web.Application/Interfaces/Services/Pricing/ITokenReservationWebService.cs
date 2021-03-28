#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.RequestResponseShared.Request.Tokens;
using FoundersPC.RequestResponseShared.Response.Tokens;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.Pricing
{
    // todo: make variation with user id
    public interface ITokenReservationWebService
    {
        Task<BuyNewTokenResponse> ReserveNewTokenAsync(TokenType type, string userEmail, string userJwtToken);
    }
}
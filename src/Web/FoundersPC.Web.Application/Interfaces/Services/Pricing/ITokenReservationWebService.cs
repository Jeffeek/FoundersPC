using System.Threading.Tasks;
using FoundersPC.RequestResponseShared.Request.Tokens;
using FoundersPC.RequestResponseShared.Response.Tokens;
using FoundersPC.WebIdentityShared;

namespace FoundersPC.Web.Application.Interfaces.Services.Pricing
{
    // todo: make variation with user id
    public interface ITokenReservationWebService
    {
        Task<BuyNewTokenResponse> ReserveNewTokenAsync(TokenType type, string userEmail, string userJwtToken);
    }
}

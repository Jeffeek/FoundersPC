#region Using namespaces

using FoundersPC.Identity.Dto;

#endregion

namespace FoundersPC.RequestResponseShared.Response.Tokens
{
    public class BuyNewTokenResponse
    {
        public bool IsBuyingSuccessful { get; set; } = false;

        public ApiAccessUserTokenReadDto Token { get; set; } = null;
    }
}
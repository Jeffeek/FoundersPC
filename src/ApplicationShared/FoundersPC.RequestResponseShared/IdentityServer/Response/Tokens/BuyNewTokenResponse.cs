#region Using namespaces

using FoundersPC.Identity.Dto;

#endregion

namespace FoundersPC.RequestResponseShared.IdentityServer.Response.Tokens
{
    public class BuyNewTokenResponse
    {
        public bool IsBuyingSuccessful { get; set; } = false;

        public AccessTokenReadDto Token { get; set; } = null;
    }
}
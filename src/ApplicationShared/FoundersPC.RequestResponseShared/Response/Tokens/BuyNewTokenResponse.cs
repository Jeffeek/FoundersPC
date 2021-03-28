#region Using namespaces

using FoundersPC.WebIdentityShared;

#endregion

namespace FoundersPC.RequestResponseShared.Response.Tokens
{
    public class BuyNewTokenResponse
    {
        public bool IsBuyingSuccessful { get; set; } = false;

        public ApplicationAccessToken Token { get; set; } = null;
    }
}
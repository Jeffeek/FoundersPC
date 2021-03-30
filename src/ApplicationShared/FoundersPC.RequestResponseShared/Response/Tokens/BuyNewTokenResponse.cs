#region Using namespaces


#endregion

using FoundersPC.Identity.Dto;

namespace FoundersPC.RequestResponseShared.Response.Tokens
{
    public class BuyNewTokenResponse
    {
        public bool IsBuyingSuccessful { get; set; } = false;

        public ApiAccessUserTokenReadDto Token { get; set; } = null;
    }
}
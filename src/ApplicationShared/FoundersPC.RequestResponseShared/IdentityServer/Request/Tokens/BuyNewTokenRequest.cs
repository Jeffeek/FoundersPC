namespace FoundersPC.RequestResponseShared.IdentityServer.Request.Tokens
{
    public class BuyNewTokenRequest
    {
        public string UserEmail { get; set; }

        public TokenType TokenType { get; set; }
    }
}
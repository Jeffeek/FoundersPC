namespace FoundersPC.RequestResponseShared.Request.Tokens
{
    public class BuyNewTokenRequest
    {
        public string UserEmail { get; set; }

        public TokenType TokenType { get; set; }
    }
}

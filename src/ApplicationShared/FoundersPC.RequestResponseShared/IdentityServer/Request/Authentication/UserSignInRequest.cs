namespace FoundersPC.RequestResponseShared.IdentityServer.Request.Authentication
{
    public class UserSignInRequest
    {
        public string LoginOrEmail { get; set; }

        public string Password { get; set; }
    }
}
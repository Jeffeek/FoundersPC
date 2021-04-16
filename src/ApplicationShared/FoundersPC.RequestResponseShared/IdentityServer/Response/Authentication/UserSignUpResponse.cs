namespace FoundersPC.RequestResponseShared.IdentityServer.Response.Authentication
{
    public class UserSignUpResponse
    {
        public bool IsRegistrationSuccessful { get; set; } = false;

        public string ResponseException { get; set; } = null;

        public string Email { get; set; } = null;

        public string Role { get; set; } = null;

        public string JwtToken { get; set; } = null;
    }
}
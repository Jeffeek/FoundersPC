namespace FoundersPC.RequestResponseShared.IdentityServer.Response.Authentication
{
    public class UserForgotPasswordResponse
    {
        public string Email { get; set; } = null;

        public bool IsUserExists { get; set; } = false;

        public bool IsConfirmationMailSent { get; set; } = false;

        public string Error { get; set; } = null;
    }
}
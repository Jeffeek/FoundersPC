namespace FoundersPC.AuthenticationShared.Response
{
    public class UserRegisterResponse
    {
        public bool IsRegistrationSuccessful { get; set; } = false;

        public string ResponseException { get; set; } = null;

        public string Email { get; set; } = null;

        public int UserId { get; set; } = -1;

        public string Role { get; set; } = null;
    }
}
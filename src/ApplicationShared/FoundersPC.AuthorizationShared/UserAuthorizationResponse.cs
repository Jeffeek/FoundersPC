namespace FoundersPC.AuthorizationShared
{
    public class UserAuthorizationResponse
    {
        public bool IsUserExists { get; set; }

        public bool IsUserBlocked { get; set; }

        public string Email { get; set; } = null;

        public string Token { get; set; } = null;
    }
}

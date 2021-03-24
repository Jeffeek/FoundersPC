namespace FoundersPC.RequestResponseShared.Response.Authentication
{
    public class UserLoginResponse
    {
        public bool IsUserExists { get; set; } = false;

        public bool IsUserActive { get; set; } = false;

        public bool IsUserBlocked { get; set; } = false;

        public string MetaInfo { get; set; } = null;

        public string Email { get; set; } = null;

        public string Role { get; set; } = null;

        public string JwtToken { get; set; } = null;
    }
}
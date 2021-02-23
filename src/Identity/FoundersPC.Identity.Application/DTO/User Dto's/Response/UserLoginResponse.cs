namespace FoundersPC.Identity.Application.DTO.Response
{
    public class UserLoginResponse
    {
        public bool IsUserExists { get; set; } = false;

        public bool IsUserActive { get; set; } = false;

        public bool IsUserBlocked { get; set; } = false;

        public string MetaInfo { get; set; } = null;

        public string Email { get; set; } = null;

        public string Login { get; set; } = null;

        public string Role { get; set; } = null;
    }
}

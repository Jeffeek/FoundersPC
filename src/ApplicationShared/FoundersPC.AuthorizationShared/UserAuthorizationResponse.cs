namespace FoundersPC.AuthorizationShared
{
    public class UserAuthorizationResponse
    {
        public bool IsUserExists { get; set; } = false;

        public bool IsUserBlocked { get; set; } = false;

        public string EmailOrLogin { get; set; } = null;

        public string RoleTitle { get; set; } = "None";
    }
}

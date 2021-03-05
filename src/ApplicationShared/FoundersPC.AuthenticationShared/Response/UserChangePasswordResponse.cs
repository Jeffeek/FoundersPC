namespace FoundersPC.AuthenticationShared.Response
{
    public class UserChangePasswordResponse
    {
        public string Email { get; set; }

        public bool Successful { get; set; } = false;

        public string Error { get; set; } = null;
    }
}

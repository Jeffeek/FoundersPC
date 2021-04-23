namespace FoundersPC.RequestResponseShared.IdentityServer.Response.Administration.Admin.Users.Blocking
{
    public class BlockUserResponse
    {
        public string AdministratorEmail { get; set; }

        public bool IsBlockingSuccessful { get; set; } = false;

        public string Error { get; set; } = null;
    }
}
namespace FoundersPC.RequestResponseShared.IdentityServer.Response.Administration.Admin.Users.Inactivity
{
    public class MakeUserInactiveResponse
    {
        public string AdministratorEmail { get; set; }

        public bool IsUserMadeInactiveSuccessful { get; set; } = false;

        public string Error { get; set; } = null;
    }
}
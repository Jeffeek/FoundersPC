namespace FoundersPC.RequestResponseShared.Response.Administration.Admin.Users.Blocking
{
    public class UnblockUserResponse
    {
        public string AdministratorEmail { get; set; }

        public bool IsUnblockingSuccessful { get; set; } = false;

        public string Error { get; set; } = null;
    }
}
namespace FoundersPC.RequestResponseShared.Response.Administration.Admin.Blocking
{
    public class UnblockUserResponse
    {
        public string AdministratorEmail { get; set; }

        public bool IsUnblockingSuccessful { get; set; } = false;

        public string Error { get; set; } = null;
    }
}
namespace FoundersPC.RequestResponseShared.Response.Administration.Admin.Blocking
{
    public class BlockUserResponse
    {
        public string AdministratorEmail { get; set; }

        public bool IsBlockingSuccessful { get; set; } = false;

        public string Error { get; set; } = null;
    }
}
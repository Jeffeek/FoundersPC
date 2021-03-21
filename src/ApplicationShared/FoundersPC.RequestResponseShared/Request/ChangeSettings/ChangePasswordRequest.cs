namespace FoundersPC.RequestResponseShared.Request.ChangeSettings
{
    public class ChangePasswordRequest
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
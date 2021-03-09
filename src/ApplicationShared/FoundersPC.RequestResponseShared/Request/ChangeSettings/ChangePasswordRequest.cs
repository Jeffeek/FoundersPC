#region Using namespaces

#endregion

namespace FoundersPC.RequestResponseShared.Request.ChangeSettings
{
    public class ChangePasswordRequest
    {
        public string Email { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
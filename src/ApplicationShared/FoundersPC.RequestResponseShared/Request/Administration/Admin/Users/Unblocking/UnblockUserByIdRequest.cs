namespace FoundersPC.RequestResponseShared.Request.Administration.Admin.Users.Unblocking
{
    public class UnblockUserByIdRequest
    {
        public int UserId { get; set; }

        public bool UnblockUserTokens { get; set; } = true;

        public bool SendNotificationToUserViaEmail { get; set; } = true;
    }
}
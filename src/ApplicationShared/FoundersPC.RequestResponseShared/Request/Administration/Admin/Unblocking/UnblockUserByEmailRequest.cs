namespace FoundersPC.RequestResponseShared.Request.Administration.Admin.Unblocking
{
    //todo: validation
    public class UnblockUserByEmailRequest
    {
        public string UserEmail { get; set; }

        public bool UnblockUserTokens { get; set; } = true;

        public bool SendNotificationToUserViaEmail { get; set; } = true;
    }
}

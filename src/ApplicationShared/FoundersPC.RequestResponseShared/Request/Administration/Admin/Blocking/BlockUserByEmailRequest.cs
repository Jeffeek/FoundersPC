namespace FoundersPC.RequestResponseShared.Request.Administration.Admin.Blocking
{
    public class BlockUserByEmailRequest
    {
        public string UserEmail { get; set; }

        public bool BlockUserTokens { get; set; } = true;

        public bool SendNotificationToUserViaEmail { get; set; } = true;
    }
}
namespace FoundersPC.RequestResponseShared.Request.Administration.Admin.Blocking
{
    public class BlockUserByIdRequest
    {
        public int UserId { get; set; }

        public bool BlockUserTokens { get; set; } = true;

        public bool SendNotificationToUserViaEmail { get; set; } = true;
    }
}

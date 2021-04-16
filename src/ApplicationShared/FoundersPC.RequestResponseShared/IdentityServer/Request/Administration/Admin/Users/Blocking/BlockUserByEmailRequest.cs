namespace FoundersPC.RequestResponseShared.IdentityServer.Request.Administration.Admin.Users.Blocking
{
    public class BlockUserByEmailRequest
    {
        public string UserEmail { get; set; }

        public bool BlockUserTokens { get; set; } = true;

        public bool SendNotificationToUserViaEmail { get; set; } = true;
    }
}
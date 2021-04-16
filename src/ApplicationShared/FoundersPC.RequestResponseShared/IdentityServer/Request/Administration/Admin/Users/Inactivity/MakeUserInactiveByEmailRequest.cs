namespace FoundersPC.RequestResponseShared.IdentityServer.Request.Administration.Admin.Users.Inactivity
{
    public class MakeUserInactiveByEmailRequest
    {
        public string UserEmail { get; set; }

        public bool SendNotificationToUserViaEmail { get; set; } = true;
    }
}
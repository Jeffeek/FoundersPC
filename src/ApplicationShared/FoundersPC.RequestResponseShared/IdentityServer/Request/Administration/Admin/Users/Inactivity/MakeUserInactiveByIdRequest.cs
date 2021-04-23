namespace FoundersPC.RequestResponseShared.IdentityServer.Request.Administration.Admin.Users.Inactivity
{
    public class MakeUserInactiveByIdRequest
    {
        public int UserId { get; set; }

        public bool SendNotificationToUserViaEmail { get; set; } = true;
    }
}
namespace FoundersPC.RequestResponseShared.Request.Administration.Admin.Inactivity
{
    public class MakeUserInactiveByEmailRequest
    {
        public string UserEmail { get; set; }

        public bool SendNotificationToUserViaEmail { get; set; } = true;
    }
}
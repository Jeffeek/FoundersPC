namespace FoundersPC.RequestResponseShared.Request.Administration.Admin.Inactivity
{
    public class MakeUserInactiveByIdRequest
    {
        public int UserId { get; set; }

        public bool SendNotificationToUserViaEmail { get; set; } = true;
    }
}

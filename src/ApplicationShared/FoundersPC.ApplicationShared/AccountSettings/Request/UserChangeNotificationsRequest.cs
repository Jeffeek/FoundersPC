namespace FoundersPC.ApplicationShared.AccountSettings.Request
{
    public class UserChangeNotificationsRequest
    {
        public int UserId { get; set; }

        public bool SendMessageOnEntrance { get; set; }

        public bool SendMessageOnApiRequest { get; set; }
    }
}
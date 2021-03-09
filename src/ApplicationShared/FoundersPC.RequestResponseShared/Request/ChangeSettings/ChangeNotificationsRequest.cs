namespace FoundersPC.RequestResponseShared.Request.ChangeSettings
{
    public class ChangeNotificationsRequest
    {
        public string Email { get; set; }

        public bool SendMessageOnEntrance { get; set; }

        public bool SendMessageOnApiRequest { get; set; }
    }
}
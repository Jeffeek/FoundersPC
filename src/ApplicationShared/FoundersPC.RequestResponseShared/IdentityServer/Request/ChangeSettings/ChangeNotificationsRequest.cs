namespace FoundersPC.RequestResponseShared.IdentityServer.Request.ChangeSettings
{
    public class ChangeNotificationsRequest
    {
        public bool SendMessageOnEntrance { get; set; }

        public bool SendMessageOnApiRequest { get; set; }
    }
}
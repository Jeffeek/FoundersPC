namespace FoundersPC.ApplicationShared.AccountSettings.Response
{
    public class UserSettingsChangeResponse
    {
        public int UserId { get; set; }

        public string Operation { get; set; }

        public bool Successful { get; set; }

        public string Error { get; set; }
    }
}
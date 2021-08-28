namespace FoundersPC.API.Application.Settings
{
    public class PasswordSettings
    {
        public string Salt { get; set; }

        public int WorkFactor { get; set; }
    }
}
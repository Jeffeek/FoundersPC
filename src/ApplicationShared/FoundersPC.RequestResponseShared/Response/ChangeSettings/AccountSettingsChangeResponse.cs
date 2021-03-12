namespace FoundersPC.RequestResponseShared.Response.ChangeSettings
{
	public class AccountSettingsChangeResponse
	{
		public string Email { get; set; }

		public string Operation { get; set; }

		public bool Successful { get; set; }

		public string Error { get; set; }
	}
}
namespace FoundersPC.RequestResponseShared.Request.Authentication
{
	public class UserSignInRequest
	{
		public string LoginOrEmail { get; set; }

		public string Password { get; set; }
	}
}
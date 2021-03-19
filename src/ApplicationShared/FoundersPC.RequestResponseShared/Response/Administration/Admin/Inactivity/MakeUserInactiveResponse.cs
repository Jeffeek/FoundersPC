namespace FoundersPC.RequestResponseShared.Response.Administration.Admin.Inactivity
{
	public class MakeUserInactiveResponse
	{
		public string AdministratorEmail { get; set; }

		public bool IsUserMadeInactiveSuccessful { get; set; } = false;

		public string Error { get; set; } = null;
	}
}

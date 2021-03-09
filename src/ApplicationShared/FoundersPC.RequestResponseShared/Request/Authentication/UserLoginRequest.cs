#region Using namespaces

using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.RequestResponseShared.Request.Authentication
{
	public class UserLoginRequest
	{
		public string LoginOrEmail { get; set; }

		public string Password { get; set; }
	}
}
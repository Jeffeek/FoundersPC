#region Using namespaces

using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.RequestResponseShared.Request.Authentication
{
	public class UserRegisterRequest
	{
		public string Email { get; set; }

		public string Password { get; set; }
	}
}
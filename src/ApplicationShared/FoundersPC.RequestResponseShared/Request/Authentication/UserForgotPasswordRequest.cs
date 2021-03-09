#region Using namespaces

using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.RequestResponseShared.Request.Authentication
{
	public class UserForgotPasswordRequest
	{
		public string Email { get; set; }
	}
}
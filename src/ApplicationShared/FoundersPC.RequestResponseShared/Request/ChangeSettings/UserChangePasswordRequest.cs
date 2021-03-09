#region Using namespaces

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.RequestResponseShared.Request.ChangeSettings
{
	public class UserChangePasswordRequest
	{
		public string Email { get; set; }

		public string OldPassword { get; set; }

		public string NewPassword { get; set; }
	}
}
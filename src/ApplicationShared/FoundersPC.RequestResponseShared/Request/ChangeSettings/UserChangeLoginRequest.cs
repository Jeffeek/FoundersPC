#region Using namespaces

using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.RequestResponseShared.Request.ChangeSettings
{
	public class UserChangeLoginRequest
	{
		public string Email { get; set; }

		public string NewLogin { get; set; }
	}
}
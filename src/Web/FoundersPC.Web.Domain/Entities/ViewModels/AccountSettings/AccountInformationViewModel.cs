#region Using namespaces

using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Web.Domain.Entities.ViewModels.AccountSettings
{
	public class AccountInformationViewModel
	{
		[EmailAddress]
		[Display(Name = "Email", Prompt = "Email")]
		public string Email { get; set; }

		[Display(Name = "Role", Prompt = "User role")]
		public string Role { get; set; }

		[Display(Name = "Login", Prompt = "User login")]
		public string Login { get; set; }
	}
}
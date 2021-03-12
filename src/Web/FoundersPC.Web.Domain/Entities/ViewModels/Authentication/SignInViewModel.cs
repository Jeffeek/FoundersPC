#region Using namespaces

using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Web.Domain.Entities.ViewModels.Authentication
{
	public class SignInViewModel
	{
		[Required]
		[Display(Name = "Email or Login",
				 Prompt = "Email or Login")]
		public string LoginOrEmail { get; set; }

		[Display(Name = "Password",
				 Prompt = "Password")]
		[StringLength(30, MinimumLength = 6)]
		[Required]
		public string RawPassword { get; set; }
	}
}
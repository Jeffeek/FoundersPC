#region Using namespaces

using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Web.Domain.Entities.ViewModels.Authentication
{
	public class SignUpViewModel
	{
		[Required]
		[Display(Name = "Email address",
				 Prompt = "Email address")]
		[EmailAddress]
		public string Email { get; set; }

		[Compare(nameof(RawPasswordConfirm),
				 ErrorMessage = "Password and Confirm Password are not equal")]
		[Display(Name = "Password",
				 Prompt = "Password")]
		[Required]
		public string RawPassword { get; set; }

		[Compare(nameof(RawPassword),
				 ErrorMessage = "Password and Confirm Password are not equal")]
		[Display(Name = "Confirm Password",
				 Prompt = "Confirm Password")]
		[Required]
		public string RawPasswordConfirm { get; set; }
	}
}
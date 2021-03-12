#region Using namespaces

using FluentValidation;
using FoundersPC.RequestResponseShared.Request.Authentication;

#endregion

namespace FoundersPC.Identity.Application.Validation.Requests.Authentication
{
	public class UserSignUpRequestValidator : AbstractValidator<UserSignUpRequest>
	{
		public UserSignUpRequestValidator()
		{
			RuleFor(model => model.Email)
					.NotNull()
					.NotEmpty()
					.EmailAddress();

			RuleFor(model => model.Password)
					.NotNull()
					.NotEmpty()
					.MinimumLength(6)
					.MaximumLength(30);
		}
	}
}
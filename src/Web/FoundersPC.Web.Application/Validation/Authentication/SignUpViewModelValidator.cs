#region Using namespaces

using FluentValidation;
using FoundersPC.Web.Domain.Entities.ViewModels.Authentication;

#endregion

namespace FoundersPC.Web.Application.Validation.Authentication
{
	public class SignUpViewModelValidator : AbstractValidator<SignUpViewModel>
	{
		public SignUpViewModelValidator()
		{
			RuleFor(model => model.Email)
					.NotNull()
					.NotEmpty()
					.EmailAddress();

			RuleFor(model => model.RawPassword)
					.NotNull()
					.NotNull()
					.MinimumLength(6)
					.MaximumLength(30)
					.Equal(model => model.RawPasswordConfirm);

			RuleFor(model => model.RawPasswordConfirm)
					.NotNull()
					.NotNull()
					.MinimumLength(6)
					.MaximumLength(30)
					.Equal(model => model.RawPassword);
		}
	}
}
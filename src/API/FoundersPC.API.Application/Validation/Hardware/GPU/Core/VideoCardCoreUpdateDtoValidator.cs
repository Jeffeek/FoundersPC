#region Using namespaces

using FluentValidation;

#endregion

namespace FoundersPC.API.Application.Validation.Hardware.GPU.Core
{
	public class VideoCardCoreUpdateDtoValidator : AbstractValidator<VideoCardCoreUpdateDto>
	{
		public VideoCardCoreUpdateDtoValidator()
		{
			RuleFor(x => x.Title)
					.NotNull()
					.NotEmpty()
					.MaximumLength(100);

			RuleFor(x => x.ArchitectureTitle)
					.NotNull()
					.NotEmpty()
					.MaximumLength(30);

			RuleFor(x => x.Codename)
					.NotNull()
					.NotEmpty()
					.MaximumLength(30);

			RuleFor(x => x.ConnectionInterface)
					.NotNull()
					.NotEmpty()
					.MaximumLength(30);

			RuleFor(x => x.DirectX)
					.MaximumLength(10);

			RuleFor(x => x.MaxResolution)
					.NotNull()
					.NotEmpty()
					.MaximumLength(20);

			RuleFor(x => x.TechProcess)
					.GreaterThan(5)
					.LessThanOrEqualTo(48);
		}
	}
}
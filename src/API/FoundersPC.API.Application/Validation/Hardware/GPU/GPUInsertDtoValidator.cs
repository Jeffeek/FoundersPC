#region Using namespaces

using FluentValidation;

#endregion

namespace FoundersPC.API.Application.Validation.Hardware.GPU
{
	public class GPUInsertDtoValidator : AbstractValidator<GPUInsertDto>
	{
		public GPUInsertDtoValidator()
		{
			RuleFor(x => x.ProducerId)
					.GreaterThan(0);

			RuleFor(x => x.Frequency)
					.GreaterThan(0);

			RuleFor(x => x.ProducerId)
					.GreaterThan(0);

			RuleFor(x => x.Series)
					.NotNull()
					.NotEmpty()
					.MaximumLength(30)
					.MinimumLength(3);

			RuleFor(x => x.Title)
					.NotNull()
					.NotEmpty()
					.MaximumLength(100);

			RuleFor(x => x.AdditionalPower)
					.GreaterThanOrEqualTo(0);

			RuleFor(x => x.DVI)
					.GreaterThanOrEqualTo(0);

			RuleFor(x => x.DisplayPort)
					.GreaterThanOrEqualTo(0);

			RuleFor(x => x.HDMI)
					.GreaterThanOrEqualTo(0);

			RuleFor(x => x.VGA)
					.GreaterThanOrEqualTo(0);

			RuleFor(x => x.GraphicsProcessorId)
					.GreaterThan(0);

			RuleFor(x => x.VideoMemoryBusWidth)
					.GreaterThan(0);

			RuleFor(x => x.VideoMemoryFrequency)
					.GreaterThan(0);

			RuleFor(x => x.VideoMemoryType)
					.NotNull()
					.NotEmpty()
					.MaximumLength(8)
					.MinimumLength(4);
		}
	}
}
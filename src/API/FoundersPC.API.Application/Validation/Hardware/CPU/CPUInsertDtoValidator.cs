#region Using namespaces

using FluentValidation;

#endregion

namespace FoundersPC.API.Application.Validation.Hardware.CPU
{
    public class CPUInsertDtoValidator : AbstractValidator<CPUInsertDto>
    {
        public CPUInsertDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Series)
                .NotNull()
                .NotEmpty()
                .MaximumLength(15)
                .MinimumLength(3);

            RuleFor(x => x.ProducerId)
                .GreaterThan(0);

            RuleFor(x => x.Frequency)
                .GreaterThan(0);

            RuleFor(x => x.Cores)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.Threads)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(512);

            RuleFor(x => x.TurboBoostFrequency)
                .GreaterThan(0);

            RuleFor(x => x.L1Cache)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.L2Cache)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.L3Cache)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.MaxRamSpeed)
                .GreaterThan(0);

            RuleFor(x => x.ProcessorCoreId)
                .GreaterThan(0);

            RuleFor(x => x.TDP)
                .GreaterThanOrEqualTo(3)
                .LessThanOrEqualTo(300);

            RuleFor(x => x.TechProcess)
                .GreaterThanOrEqualTo(5)
                .LessThanOrEqualTo(48);
        }
    }
}
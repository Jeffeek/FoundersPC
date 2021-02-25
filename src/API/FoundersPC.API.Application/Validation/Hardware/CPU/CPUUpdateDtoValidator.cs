#region Using namespaces

using FluentValidation;

#endregion

namespace FoundersPC.API.Application.Validation.Hardware.CPU
{
    public class CPUUpdateDtoValidator : AbstractValidator<CPUUpdateDto>
    {
        public CPUUpdateDtoValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(100);
            RuleFor(x => x.Series).NotNull().NotEmpty().MaximumLength(15);
            RuleFor(x => x.ProducerId).GreaterThan(0);
            RuleFor(x => x.Frequency).GreaterThan(0);
            RuleFor(x => x.Cores).GreaterThanOrEqualTo(1);
            RuleFor(x => x.Threads).GreaterThan(1);
            RuleFor(x => x.TurboBoostFrequency).GreaterThan(0);
            RuleFor(x => x.L1Cache).GreaterThanOrEqualTo(0);
            RuleFor(x => x.L2Cache).GreaterThanOrEqualTo(0);
            RuleFor(x => x.L3Cache).GreaterThanOrEqualTo(0);
            RuleFor(x => x.MaxRamSpeed).GreaterThan(0);
            RuleFor(x => x.ProcessorCoreId).GreaterThan(0);
            RuleFor(x => x.TDP).GreaterThan(0);
            RuleFor(x => x.TechProcess).GreaterThan(7);
        }
    }
}
#region Using namespaces

using FluentValidation;

#endregion

namespace FoundersPC.API.Application.Validation.Hardware.CPU.Core
{
    public class ProcessorCoreUpdateDtoValidation : AbstractValidator<ProcessorCoreUpdateDto>
    {
        public ProcessorCoreUpdateDtoValidation()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x => x.L2Cache).GreaterThanOrEqualTo(0);
            RuleFor(x => x.L3Cache).GreaterThanOrEqualTo(0);
            RuleFor(x => x.MicroArchitecture).NotNull().NotEmpty().MaximumLength(30);
            RuleFor(x => x.Socket).NotNull().NotEmpty().MaximumLength(10);
        }
    }
}
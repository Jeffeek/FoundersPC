#region Using namespaces

using FluentValidation;

#endregion

namespace FoundersPC.API.Dto.HardwareValidation.Hardware.CPU.Core
{
    public class ProcessorCoreInsertDtoValidator : AbstractValidator<ProcessorCoreInsertDto>
    {
        public ProcessorCoreInsertDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .MinimumLength(3);

            RuleFor(x => x.L2Cache)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.L3Cache)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.MicroArchitecture)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30)
                .MinimumLength(3);

            RuleFor(x => x.Socket)
                .NotNull()
                .NotEmpty()
                .MaximumLength(10)
                .MinimumLength(4);
        }
    }
}
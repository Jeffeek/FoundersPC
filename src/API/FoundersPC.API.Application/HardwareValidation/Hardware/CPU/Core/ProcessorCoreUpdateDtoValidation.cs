#region Using namespaces

using FluentValidation;
using FoundersPC.API.Dto;

#endregion

namespace FoundersPC.API.Application.HardwareValidation.Hardware.CPU.Core
{
    /// <summary>
    ///     Validator for <see cref="ProcessorCoreUpdateDto"/>
    /// </summary>
    public class ProcessorCoreUpdateDtoValidation : AbstractValidator<ProcessorCoreUpdateDto>
    {
        public ProcessorCoreUpdateDtoValidation()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100)
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
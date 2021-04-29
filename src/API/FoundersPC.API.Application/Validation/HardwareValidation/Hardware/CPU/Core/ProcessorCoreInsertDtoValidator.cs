#region Using namespaces

using FluentValidation;
using FoundersPC.API.Dto;

#endregion

namespace FoundersPC.API.Application.Validation.HardwareValidation.Hardware.CPU.Core
{
    /// <inheritdoc/>
    /// <summary>
    ///     Validator for <see cref="T:FoundersPC.API.Dto.ProcessorCoreInsertDto"/>
    /// </summary>
    public class ProcessorCoreInsertDtoValidator : AbstractValidator<ProcessorCoreInsertDto>
    {
        public ProcessorCoreInsertDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100)
                .MinimumLength(3);

            RuleFor(x => x.L2CachePerCore)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.L3CachePerCore)
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
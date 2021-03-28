#region Using namespaces

using FluentValidation;
using FoundersPC.API.Dto;

#endregion

namespace FoundersPC.API.Application.Validation.Hardware.CPU.Core
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
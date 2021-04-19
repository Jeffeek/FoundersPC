#region Using namespaces

using FluentValidation;
using FoundersPC.API.Dto;

#endregion

namespace FoundersPC.API.Application.HardwareValidation.Hardware.Memory.SSD
{
    public class SSDInsertDtoValidator : AbstractValidator<SolidStateDriveInsertDto>
    {
        public SSDInsertDtoValidator()
        {
            RuleFor(x => x.ProducerId)
                .GreaterThan(0);

            RuleFor(x => x.Interface)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(20);

            RuleFor(x => x.Factor)
                .InclusiveBetween(2.5, 3.5)
                .When(x => x?.Interface != "M.2");

            RuleFor(x => x.Volume)
                .InclusiveBetween(30, 10000);

            RuleFor(x => x.SequentialRecording)
                .GreaterThan(0);

            RuleFor(x => x.MicroScheme)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(50);

            RuleFor(x => x.SequentialRead)
                .GreaterThan(0);

            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
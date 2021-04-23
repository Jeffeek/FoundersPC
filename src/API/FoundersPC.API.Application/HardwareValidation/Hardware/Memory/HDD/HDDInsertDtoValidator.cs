#region Using namespaces

using FluentValidation;
using FoundersPC.API.Dto;

#endregion

namespace FoundersPC.API.Application.HardwareValidation.Hardware.Memory.HDD
{
    public class HDDInsertDtoValidator : AbstractValidator<HardDriveDiskInsertDto>
    {
        public HDDInsertDtoValidator()
        {
            RuleFor(x => x.ProducerId)
                .GreaterThan(0);

            RuleFor(x => x.Factor)
                .Must(x => x.Equals(2.5d) || x.Equals(3.5d));

            RuleFor(x => x.Interface)
                .NotNull()
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Volume)
                .GreaterThanOrEqualTo(60)
                .LessThanOrEqualTo(24576);

            RuleFor(x => x.Noise)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100);

            RuleFor(x => x.BufferSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(1024);

            RuleFor(x => x.HeadSpeed)
                .Must(x => x == 5400 || x == 7200);
        }
    }
}
#region Using namespaces

using FluentValidation;
using FoundersPC.API.Dto;

#endregion

namespace FoundersPC.API.Application.HardwareValidation.Hardware.Memory.RAM
{
    public class RAMInsertDtoValidator : AbstractValidator<RandomAccessMemoryInsertDto>
    {
        public RAMInsertDtoValidator()
        {
            RuleFor(x => x.ProducerId)
                .GreaterThan(0);

            RuleFor(x => x.MemoryType)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(15);

            RuleFor(x => x.Frequency)
                .GreaterThan(0)
                .LessThanOrEqualTo(8666);

            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.CASLatency)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(5);

            RuleFor(x => x.PCIndex)
                .GreaterThan(0);

            RuleFor(x => x.Voltage)
                .GreaterThan(1)
                .LessThan(2);

            RuleFor(x => x.Timings)
                .NotNull()
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(11)
                .Matches("^\\d{1,2}(-\\d{1,2}-\\d{1,2}(-\\d{1,2})?)?$");
        }
    }
}
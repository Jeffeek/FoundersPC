#region Using namespaces

using FluentValidation;

#endregion

namespace FoundersPC.API.Application.Validation.Hardware.Memory.HDD
{
    public class HDDUpdateDtoValidator : AbstractValidator<HDDUpdateDto>
    {
        public HDDUpdateDtoValidator()
        {
            RuleFor(x => x.ProducerId)
                .GreaterThan(0);

            RuleFor(x => x.Factor)
                .InclusiveBetween(2.5, 3.5);

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
                .InclusiveBetween(5400, 7200);
        }
    }
}
#region Using namespaces

using FluentValidation;
using FoundersPC.API.Dto;

#endregion

namespace FoundersPC.API.Application.HardwareValidation.Hardware.Motherboard
{
    public class MotherboardInsertDtoValidation : AbstractValidator<MotherboardInsertDto>
    {
        public MotherboardInsertDtoValidation()
        {
            RuleFor(x => x.ProducerId)
                .GreaterThan(0);

            RuleFor(x => x.Factor)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(10);

            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Socket)
                .NotNull()
                .NotEmpty()
                .MaximumLength(3)
                .MaximumLength(10);

            RuleFor(x => x.RAMSupport)
                .NotNull()
                .NotEmpty()
                .MinimumLength(4)
                .MaximumLength(7)
                .Matches(_ => "DDR\\d(\\w+)?");

            RuleFor(x => x.AudioSupport)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(20);

            RuleFor(x => x.RAMSlots)
                .InclusiveBetween(1, 6);

            RuleFor(x => x.RAMMode)
                .NotNull()
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(5);

            RuleFor(x => x.M2SlotsCount)
                .InclusiveBetween(0, 6);

            RuleFor(x => x.PCIExpressVersion)
                .NotNull()
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(12);
        }
    }
}
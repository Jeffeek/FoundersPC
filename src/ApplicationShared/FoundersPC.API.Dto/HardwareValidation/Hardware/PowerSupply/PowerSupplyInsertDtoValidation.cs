#region Using namespaces

using FluentValidation;

#endregion

namespace FoundersPC.API.Dto.HardwareValidation.Hardware.PowerSupply
{
    public class PowerSupplyInsertDtoValidation : AbstractValidator<PowerSupplyInsertDto>
    {
        public PowerSupplyInsertDtoValidation()
        {
            RuleFor(x => x.ProducerId)
                .GreaterThan(0);

            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Power)
                .InclusiveBetween(50, 10000);

            RuleFor(x => x.Efficiency)
                .InclusiveBetween(50, 100)
                .When(x => x.Efficiency != null);

            RuleFor(x => x.MotherboardPowering)
                .NotNull()
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(10)
                .Matches("2[0,4](\\+[4,8])?");

            RuleFor(x => x.FanDiameter)
                .InclusiveBetween(0, 300);
        }
    }
}
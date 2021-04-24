#region Using namespaces

using FluentValidation;
using FoundersPC.API.Dto;

#endregion

namespace FoundersPC.API.Application.HardwareValidation.Hardware.PowerSupply
{
    /// <summary>
    ///     Validator for <see cref="PowerSupplyInsertDto"/>
    /// </summary>
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
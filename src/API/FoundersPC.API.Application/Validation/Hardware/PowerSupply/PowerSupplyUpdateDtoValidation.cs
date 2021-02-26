using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace FoundersPC.API.Application.Validation.Hardware.PowerSupply
{
    public class PowerSupplyUpdateDtoValidation : AbstractValidator<PowerSupplyUpdateDto>
    {
        public PowerSupplyUpdateDtoValidation()
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
                .MaximumLength(10);
            RuleFor(x => x.FanDiameter)
                .InclusiveBetween(0, 300);
        }
    }
}

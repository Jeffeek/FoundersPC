using System;
using System.Linq;
using FluentValidation;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Domain.Enums;

namespace FoundersPC.Application.Features.Hardware.Validators;

public class DeleteRequestBaseValidator<TRequest> : AbstractValidator<TRequest>
    where TRequest : DeleteRequest
{
    protected DeleteRequestBaseValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.HardwareTypeId)
            .Must(x => Enum.GetValues<HardwareType>()
                           .Any(z => (int)z == x))
            .WithMessage("Hardware Type Id was outside of valid Id's");
    }
}
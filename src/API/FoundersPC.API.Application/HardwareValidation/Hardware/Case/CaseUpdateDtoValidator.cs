#region Using namespaces

using FluentValidation;
using FoundersPC.API.Dto;

#endregion

namespace FoundersPC.API.Application.HardwareValidation.Hardware.Case
{
    /// <summary>
    ///     Validator for <see cref="CaseUpdateDto"/>
    /// </summary>
    public class CaseUpdateDtoValidator : AbstractValidator<CaseUpdateDto>
    {
        public CaseUpdateDtoValidator()
        {
            RuleFor(x => x.Type)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40)
                .MinimumLength(3);

            RuleFor(x => x.MaxMotherboardSize)
                .NotNull()
                .NotEmpty()
                .MaximumLength(20)
                .MinimumLength(3);

            RuleFor(x => x.Material)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .MinimumLength(3);

            RuleFor(x => x.WindowMaterial)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .MinimumLength(3);

            RuleFor(x => x.Color)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .MinimumLength(2);

            RuleFor(x => x.ProducerId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Width)
                .GreaterThan(0)
                .When(x => x.Width is not null);

            RuleFor(x => x.Depth)
                .GreaterThan(0)
                .When(x => x.Depth is not null);

            RuleFor(x => x.Height)
                .GreaterThan(0)
                .When(x => x.Height is not null);

            RuleFor(x => x.Weight)
                .GreaterThan(0)
                .When(x => x.Weight is not null);
        }
    }
}
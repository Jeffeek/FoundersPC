#region Using namespaces

using FluentValidation;

#endregion

namespace FoundersPC.API.Application.Validation.Hardware.Case
{
    public class CaseUpdateDtoValidation : AbstractValidator<CaseUpdateDto>
    {
        public CaseUpdateDtoValidation()
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
        }
    }
}
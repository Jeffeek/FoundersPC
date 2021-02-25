#region Using namespaces

using FluentValidation;

#endregion

namespace FoundersPC.API.Application.Validation.Hardware.Case
{
    public class CaseInsertDtoValidation : AbstractValidator<CaseInsertDto>
    {
        public CaseInsertDtoValidation()
        {
            RuleFor(x => x.Type).Cascade(CascadeMode.Stop).NotNull().NotEmpty().MaximumLength(40);
            RuleFor(x => x.MaxMotherboardSize).Cascade(CascadeMode.Stop).NotNull().NotEmpty().MaximumLength(20);
            RuleFor(x => x.Material).Cascade(CascadeMode.Stop).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x => x.WindowMaterial).Cascade(CascadeMode.Stop).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x => x.Color).Cascade(CascadeMode.Stop).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x => x.ProducerId).Cascade(CascadeMode.Stop).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Title).Cascade(CascadeMode.Stop).NotNull().NotEmpty().MaximumLength(100);
        }
    }
}
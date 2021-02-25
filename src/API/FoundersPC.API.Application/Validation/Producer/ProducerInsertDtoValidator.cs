#region Using namespaces

using System;
using FluentValidation;

#endregion

namespace FoundersPC.API.Application.Validation.Producer
{
    public class ProducerInsertDtoValidator : AbstractValidator<ProducerInsertDto>
    {
        public ProducerInsertDtoValidator()
        {
            RuleFor(x => x.Country).Cascade(CascadeMode.Stop).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(x => x.FullName).Cascade(CascadeMode.Stop).NotNull().NotEmpty().MinimumLength(100);
            RuleFor(x => x.ShortName).Cascade(CascadeMode.Stop).MaximumLength(20);
            RuleFor(x => x.Website).Cascade(CascadeMode.Stop).MaximumLength(100);
            RuleFor(x => x.FoundationDate).Cascade(CascadeMode.Stop).LessThan(DateTime.Now);
        }
    }
}
#region Using namespaces

using System;
using FluentValidation;
using FoundersPC.API.Dto;

#endregion

namespace FoundersPC.API.Application.Validation.Producer
{
    public class ProducerUpdateDtoValidator : AbstractValidator<ProducerUpdateDto>
    {
        public ProducerUpdateDtoValidator()
        {
            RuleFor(x => x.Country)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.FullName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(2)
                .When(x => x != null);

            RuleFor(x => x.ShortName)
                .MaximumLength(20);

            RuleFor(x => x.Website)
                .Matches(x => "^https?://")
                .When(x => x != null)
                .MaximumLength(100)
                .When(x => x != null);

            RuleFor(x => x.FoundationDate)
                .LessThan(DateTime.Now)
                .When(x => x != null);
        }
    }
}
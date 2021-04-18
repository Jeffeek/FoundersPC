#region Using namespaces

using System;
using FluentValidation;

#endregion

namespace FoundersPC.API.Dto.HardwareValidation.Producer
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
                .MaximumLength(100)
                .When(x => x.FullName != null);

            RuleFor(x => x.ShortName)
                .MaximumLength(20);

            RuleFor(x => x.Website)
                .Matches(_ => "^https?://")
                .When(x => x.Website != null)
                .MaximumLength(100)
                .When(x => x.Website != null);

            RuleFor(x => x.FoundationDate)
                .LessThan(DateTime.Now)
                .When(x => x.FoundationDate != null);
        }
    }
}
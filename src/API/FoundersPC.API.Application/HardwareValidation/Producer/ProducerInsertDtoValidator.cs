#region Using namespaces

using System;
using FluentValidation;
using FoundersPC.API.Dto;

#endregion

namespace FoundersPC.API.Application.HardwareValidation.Producer
{
    public class ProducerInsertDtoValidator : AbstractValidator<ProducerInsertDto>
    {
        public ProducerInsertDtoValidator()
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
                .When(x => x != null);

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
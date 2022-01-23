using FluentValidation;

namespace FoundersPC.Application.Features.Producer;

public class CreateRequestValidator : AbstractValidator<CreateRequest>
{
    public CreateRequestValidator()
    {
        RuleFor(x => x.Id)
            .Equal(0);

        RuleFor(x => x.FullName)
            .NotNull()
            .NotEmpty();
    }
}
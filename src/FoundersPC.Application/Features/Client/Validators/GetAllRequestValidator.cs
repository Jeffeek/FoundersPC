using FluentValidation;
using FoundersPC.SharedKernel.Filter;

namespace FoundersPC.Application.Features.Client.Validators;

public abstract class GetAllRequestValidator<TRequest> : AbstractValidator<TRequest>
    where TRequest : SortedPagedFilter
{
    protected GetAllRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(0)
            .LessThan(30);
    }
}
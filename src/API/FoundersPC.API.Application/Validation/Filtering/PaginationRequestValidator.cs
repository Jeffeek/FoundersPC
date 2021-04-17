﻿#region Using namespaces

using FluentValidation;
using FoundersPC.RequestResponseShared.Pagination.Requests;

#endregion

namespace FoundersPC.API.Application.Validation.Filtering
{
    public class PaginationRequestValidator : AbstractValidator<PaginationRequest>
    {
        public PaginationRequestValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1);
        }
    }
}
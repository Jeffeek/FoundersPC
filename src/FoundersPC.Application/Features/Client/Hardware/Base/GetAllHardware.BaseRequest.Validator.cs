using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;

namespace FoundersPC.Application.Features.Client.Hardware.Base;

public class GetAllHardwareRequestValidator<TRequest> : AbstractValidator<TRequest>
    where TRequest : GetAllHardwareRequest
{
    private readonly List<string> _sortColumnList = new()
                                                    {
                                                        "Id",
                                                        "HardwareTypeId",
                                                        "HardwareType",
                                                        "Title",
                                                        "Created",
                                                        "LastModified",
                                                        "Producer"
                                                    };

    protected GetAllHardwareRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.SortColumn)
            .Must(x => _sortColumnList.Any(z => String.Equals(x, z, StringComparison.OrdinalIgnoreCase)))
            .WithMessage(x => $"Sort Column [{x.SortColumn}] not exists");
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FoundersPC.Application.Features.Client.Validators;

namespace FoundersPC.Application.Features.Client.Hardware.Base;

public class GetAllHardwareRequestValidator<TRequest> : GetAllRequestValidator<TRequest>
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
        RuleFor(x => x.SortColumn)
            .Must(x => _sortColumnList.Any(z => String.Equals(x, z, StringComparison.OrdinalIgnoreCase)))
            .WithMessage(x => $"Sort Column [{x.SortColumn}] not exists");
    }
}
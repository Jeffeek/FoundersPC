﻿using FoundersPC.SharedKernel.Filter;

namespace FoundersPC.Application.Features.Hardware.Base;

public abstract class GetAllHardwareRequest : SortedPagedFilter
{
    public bool? ShowDeleted { get; set; }
    public string? SearchText { get; set; }
}
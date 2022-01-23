using FoundersPC.SharedKernel.Filter;

namespace FoundersPC.Application.Features.Client.Hardware.Base;

public abstract class GetAllHardwareRequest : SortedPagedFilter
{
    public string? SearchText { get; set; }
}
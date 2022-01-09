using FoundersPC.SharedKernel.Filter;

namespace FoundersPC.Application.Features.Hardware.Base;

public abstract class GetAllHardwareRequest : SortedPagedFilter
{
    public bool? IsDeleted { get; set; }
    public string? SearchText { get; set; }
}
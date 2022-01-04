#region Using namespaces

using FoundersPC.Domain.Entities.Hardware.Metadata;

#endregion

namespace FoundersPC.Domain.Entities.Hardware;

public class Case : Hardware
{
    public CaseMetadata Metadata { get; set; } = default!;
}
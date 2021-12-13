#region Using namespaces

using System;
using FoundersPC.Domain.Entities.Hardware.Metadata;
using FoundersPC.Domain.Entities.Metadata;

#endregion

namespace FoundersPC.Domain.Entities.Hardware;

public class Case : Hardware
{
    public CaseMetadata Metadata { get; set; } = default!;
}
#region Using namespaces

using System;
using System.Collections.Generic;
using FoundersPC.Domain.Common;
using FoundersPC.Domain.Entities.Hardware.Metadata;
using FoundersPC.Domain.Entities.Metadata;

#endregion

namespace FoundersPC.Domain.Entities.Hardware;

public class Producer : FullAuditable, IIdentityItem
{
    public ICollection<HardDriveDiskMetadata>? HardDrivesMetadata { get; set; } = default!;
    public ICollection<SolidStateDriveMetadata>? SolidStateDriveMetadata { get; set; } = default!;
    public ICollection<RandomAccessMemoryMetadata>? RandomAccessMemoryMetadata { get; set; } = default!;
    public ICollection<CaseMetadata>? CasesMetadata { get; set; } = default!;
    public ICollection<ProcessorMetadata>? ProcessorsMetadata { get; set; } = default!;
    public ICollection<VideoCardMetadata>? VideoCardsMetadata { get; set; } = default!;
    public ICollection<MotherboardMetadata>? MotherboardsMetadata { get; set; } = default!;
    public ICollection<PowerSupplyMetadata>? PowerSuppliesMetadata { get; set; } = default!;

    public string? ShortName { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public int? CountryId { get; set; } = default!;
    public string? Website { get; set; } = default!;
    public DateTime? FoundationDate { get; set; } = default!;
    public Country? Country { get; set; } = default!;
    public int Id { get; set; }
}
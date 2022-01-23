using System.Collections.Generic;
using FoundersPC.Domain.Entities.Hardware.Metadata;

namespace FoundersPC.Domain.Entities.Metadata;

public class Socket : MetadataEntity
{
    public ICollection<MotherboardMetadata>? Motherboards { get; set; }
    public ICollection<ProcessorMetadata>? Processors { get; set; }
}
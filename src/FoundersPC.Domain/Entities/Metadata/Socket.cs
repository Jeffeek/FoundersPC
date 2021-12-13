using System.Collections.Generic;
using FoundersPC.Domain.Entities.Hardware;
using FoundersPC.Domain.Entities.Hardware.Metadata;

namespace FoundersPC.Domain.Entities.Metadata;

public class Socket : MetadataEntity
{
    public ICollection<Motherboard>? Motherboards { get; set; }
    public ICollection<Processor>? Processors { get; set; }
}
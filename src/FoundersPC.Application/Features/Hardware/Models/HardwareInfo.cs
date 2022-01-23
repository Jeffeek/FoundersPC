using System;
using FoundersPC.Application.Features.Producer.Models;

namespace FoundersPC.Application.Features.Hardware.Models;

public abstract class HardwareInfo
{
    public int Id { get; set; }
    public int HardwareTypeId { get; set; }
    public int ProducerId { get; set; }
    public string Title { get; set; } = default!;
    public ProducerInfo? Producer { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; } = default!;
    public DateTime LastModified { get; set; }
    public string LastModifiedBy { get; set; } = default!;
}
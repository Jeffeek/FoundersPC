using System;
using FoundersPC.Application.Features.Producer.Models;

namespace FoundersPC.Application.Features.Hardware.Models;

public class HardwareViewInfo
{
    public int Id { get; set; }
    public int HardwareTypeId { get; set; }
    public string HardwareType { get; set; } = default!;
    public string Title { get; set; } = default!;
    public ProducerViewInfo Producer { get; set; } = default!;
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; } = default!;
    public DateTime LastModified { get; set; }
    public string LastModifiedBy { get; set; } = default!;
    public bool IsDeleted { get; set; }
}
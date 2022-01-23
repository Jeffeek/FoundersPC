using System;
using FoundersPC.Application.Features.Client.Producer.Models;

namespace FoundersPC.Application.Features.Client.Hardware.Models;

public abstract class ClientHardwareInfo
{
    public int Id { get; set; }
    public int HardwareTypeId { get; set; }
    public string Title { get; set; } = default!;
    public ClientProducerInfo? Producer { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastModified { get; set; }
}
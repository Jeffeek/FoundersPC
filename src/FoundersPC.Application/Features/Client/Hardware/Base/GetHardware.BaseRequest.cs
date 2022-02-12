using Newtonsoft.Json;

namespace FoundersPC.Application.Features.Client.Hardware.Base;

public abstract class GetHardwareRequest
{
    public int Id { get; set; }
    [JsonIgnore]
    public int HardwareTypeId { get; set; }
}
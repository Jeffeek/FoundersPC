using NSwag.Annotations;

namespace FoundersPC.Application.Features.Client.Hardware.Base;

public abstract class GetHardwareRequest
{
    public int Id { get; set; }
    [OpenApiIgnore]
    public int HardwareTypeId { get; set; }
}
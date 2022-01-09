namespace FoundersPC.Application.Features.Hardware.Base;

public abstract class GetHardwareRequest
{
    public int Id { get; set; }
    public int HardwareTypeId { get; set; }
}
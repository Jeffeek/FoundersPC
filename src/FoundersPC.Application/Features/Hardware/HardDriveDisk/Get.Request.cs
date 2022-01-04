using FoundersPC.Application.Features.Hardware.HardDriveDisk.Models;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.HardDriveDisk;

public class GetRequest : IRequest<HardDriveDiskInfo>
{
    public int Id { get; set; }
}
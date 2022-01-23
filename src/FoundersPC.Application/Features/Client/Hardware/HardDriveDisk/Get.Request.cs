using FoundersPC.Application.Features.Client.Hardware.HardDriveDisk.Models;
using MediatR;

namespace FoundersPC.Application.Features.Client.Hardware.HardDriveDisk;

public class GetRequest : Base.GetHardwareRequest, IRequest<ClientHardDriveDiskInfo> { }
using FoundersPC.Application.Features.Hardware.HardDriveDisk.Models;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.HardDriveDisk;

public class RestoreRequest : Base.RestoreRequest, IRequest<HardDriveDiskInfo> { }
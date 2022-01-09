using FoundersPC.Application.Features.Hardware.SolidStateDrive.Models;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.SolidStateDrive;

public class GetRequest : Base.GetHardwareRequest, IRequest<SolidStateDriveInfo> { }
using FoundersPC.Application.Features.Hardware.SolidStateDrive.Models;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.SolidStateDrive;

public class RestoreRequest : Base.RestoreRequest, IRequest<SolidStateDriveInfo> { }
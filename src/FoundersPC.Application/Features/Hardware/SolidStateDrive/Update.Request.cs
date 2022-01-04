using FoundersPC.Application.Features.Hardware.SolidStateDrive.Models;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.SolidStateDrive;

public class UpdateRequest : SolidStateDriveInfo, IRequest<SolidStateDriveInfo> { }
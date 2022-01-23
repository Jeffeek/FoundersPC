using FoundersPC.Application.Features.Client.Hardware.SolidStateDrive.Models;
using MediatR;

namespace FoundersPC.Application.Features.Client.Hardware.SolidStateDrive;

public class GetRequest : Base.GetHardwareRequest, IRequest<ClientSolidStateDriveInfo> { }
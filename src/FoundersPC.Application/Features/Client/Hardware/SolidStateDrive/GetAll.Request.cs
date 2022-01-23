using FoundersPC.Application.Features.Client.Hardware.Base;
using FoundersPC.Application.Features.Client.Hardware.SolidStateDrive.Models;
using FoundersPC.SharedKernel.Pagination;
using MediatR;

namespace FoundersPC.Application.Features.Client.Hardware.SolidStateDrive;

public class GetAllRequest : GetAllHardwareRequest, IRequest<IPagedList<ClientSolidStateDriveInfo>> { }
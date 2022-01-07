using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.SolidStateDrive.Models;
using FoundersPC.SharedKernel.Pagination;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.SolidStateDrive;

public class GetAllRequest : GetAllHardwareRequest, IRequest<IPagedList<SolidStateDriveViewInfo>> { }
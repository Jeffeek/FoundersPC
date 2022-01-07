using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.HardDriveDisk.Models;
using FoundersPC.SharedKernel.Pagination;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.HardDriveDisk;

public class GetAllRequest : GetAllHardwareRequest, IRequest<IPagedList<HardDriveDiskViewInfo>> { }
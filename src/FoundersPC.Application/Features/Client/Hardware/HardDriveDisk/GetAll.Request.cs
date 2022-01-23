using FoundersPC.Application.Features.Client.Hardware.Base;
using FoundersPC.Application.Features.Client.Hardware.HardDriveDisk.Models;
using FoundersPC.SharedKernel.Pagination;
using MediatR;

namespace FoundersPC.Application.Features.Client.Hardware.HardDriveDisk;

public class GetAllRequest : GetAllHardwareRequest, IRequest<IPagedList<ClientHardDriveDiskInfo>> { }
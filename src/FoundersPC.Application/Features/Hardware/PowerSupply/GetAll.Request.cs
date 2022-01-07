using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.PowerSupply.Models;
using FoundersPC.SharedKernel.Pagination;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.PowerSupply;

public class GetAllRequest : GetAllHardwareRequest, IRequest<IPagedList<PowerSupplyViewInfo>> { }
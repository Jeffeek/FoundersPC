using FoundersPC.Application.Features.Client.Hardware.Base;
using FoundersPC.Application.Features.Client.Hardware.PowerSupply.Models;
using FoundersPC.SharedKernel.Pagination;
using MediatR;

namespace FoundersPC.Application.Features.Client.Hardware.PowerSupply;

public class GetAllRequest : GetAllHardwareRequest, IRequest<IPagedList<ClientPowerSupplyInfo>> { }
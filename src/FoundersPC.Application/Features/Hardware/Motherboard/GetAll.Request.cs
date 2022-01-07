using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.Motherboard.Models;
using FoundersPC.SharedKernel.Pagination;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.Motherboard;

public class GetAllRequest : GetAllHardwareRequest, IRequest<IPagedList<MotherboardViewInfo>> { }
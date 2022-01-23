using FoundersPC.Application.Features.Client.Hardware.Base;
using FoundersPC.Application.Features.Client.Hardware.Motherboard.Models;
using FoundersPC.SharedKernel.Pagination;
using MediatR;

namespace FoundersPC.Application.Features.Client.Hardware.Motherboard;

public class GetAllRequest : GetAllHardwareRequest, IRequest<IPagedList<ClientMotherboardInfo>> { }
using FoundersPC.Application.Features.Client.Hardware.Base;
using FoundersPC.Application.Features.Client.Hardware.Case.Models;
using FoundersPC.SharedKernel.Pagination;
using MediatR;

namespace FoundersPC.Application.Features.Client.Hardware.Case;

public class GetAllRequest : GetAllHardwareRequest, IRequest<IPagedList<ClientCaseInfo>> { }
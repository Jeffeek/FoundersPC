using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.Case.Models;
using FoundersPC.SharedKernel.Pagination;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.Case;

public class GetAllRequest : GetAllHardwareRequest, IRequest<IPagedList<CaseViewInfo>> { }
using FoundersPC.Application.Features.Hardware.Case.Models;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.Case;

public class GetRequest : Base.GetHardwareRequest, IRequest<CaseInfo> { }
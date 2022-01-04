using FoundersPC.Application.Features.Hardware.Case.Models;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.Case;

public class UpdateRequest : CaseInfo, IRequest<CaseInfo> { }
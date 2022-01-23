using FoundersPC.Application.Features.Client.Hardware.Case.Models;
using MediatR;

namespace FoundersPC.Application.Features.Client.Hardware.Case;

public class GetRequest : Base.GetHardwareRequest, IRequest<ClientCaseInfo> { }
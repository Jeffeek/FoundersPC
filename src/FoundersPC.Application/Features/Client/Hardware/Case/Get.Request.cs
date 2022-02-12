using FoundersPC.Application.Features.Client.Hardware.Case.Models;
using FoundersPC.Domain.Enums;
using MediatR;

namespace FoundersPC.Application.Features.Client.Hardware.Case;

public class GetRequest : Base.GetHardwareRequest, IRequest<ClientCaseInfo>
{
    public GetRequest() => HardwareTypeId = (int)HardwareType.Case;
}
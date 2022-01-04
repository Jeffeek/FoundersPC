using FoundersPC.Application.Features.Hardware.Case.Models;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.Case;

public class GetRequest : IRequest<CaseInfo>
{
    public int Id { get; set; }
}
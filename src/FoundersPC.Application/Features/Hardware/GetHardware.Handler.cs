using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application.Features.Hardware.Models;
using FoundersPC.Domain.Enums;
using MediatR;

namespace FoundersPC.Application.Features.Hardware;

public class GetHardwareHandler : IRequestHandler<GetHardwareRequest, HardwareInfo>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetHardwareHandler(IMediator mediator,
                              IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<HardwareInfo> Handle(GetHardwareRequest request, CancellationToken cancellationToken) =>
        request.HardwareTypeId switch
        {
            (int)HardwareType.Case        => await _mediator.Send(_mapper.Map<Case.GetRequest>(request), cancellationToken),
            (int)HardwareType.HDD         => await _mediator.Send(_mapper.Map<HardDriveDisk.GetRequest>(request), cancellationToken),
            (int)HardwareType.Motherboard => await _mediator.Send(_mapper.Map<Motherboard.GetRequest>(request), cancellationToken),
            (int)HardwareType.PowerSupply => await _mediator.Send(_mapper.Map<PowerSupply.GetRequest>(request), cancellationToken),
            (int)HardwareType.CPU         => await _mediator.Send(_mapper.Map<Processor.GetRequest>(request), cancellationToken),
            (int)HardwareType.RAM         => await _mediator.Send(_mapper.Map<RandomAccessMemory.GetRequest>(request), cancellationToken),
            (int)HardwareType.SSD         => await _mediator.Send(_mapper.Map<SolidStateDrive.GetRequest>(request), cancellationToken),
            (int)HardwareType.GPU         => await _mediator.Send(_mapper.Map<VideoCard.GetRequest>(request), cancellationToken),
            _                             => throw new ArgumentOutOfRangeException(nameof(request.HardwareTypeId))
        };
}
using FoundersPC.Application.Features.Hardware.Motherboard.Models;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.Motherboard;

public class GetRequest : Base.GetHardwareRequest, IRequest<MotherboardInfo> { }
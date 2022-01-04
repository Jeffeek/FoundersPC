using FoundersPC.Application.Features.Hardware.Motherboard.Models;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.Motherboard;

public class UpdateRequest : MotherboardInfo, IRequest<MotherboardInfo> { }
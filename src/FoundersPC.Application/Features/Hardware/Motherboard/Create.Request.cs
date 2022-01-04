using FoundersPC.Application.Features.Hardware.Motherboard.Models;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.Motherboard;

public class CreateRequest : MotherboardInfo, IRequest<MotherboardInfo> { }
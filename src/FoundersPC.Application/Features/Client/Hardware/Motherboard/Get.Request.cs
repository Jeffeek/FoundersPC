using FoundersPC.Application.Features.Client.Hardware.Motherboard.Models;
using MediatR;

namespace FoundersPC.Application.Features.Client.Hardware.Motherboard;

public class GetRequest : Base.GetHardwareRequest, IRequest<ClientMotherboardInfo> { }
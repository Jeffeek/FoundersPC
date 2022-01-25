﻿using FoundersPC.Application.Features.Hardware.Motherboard.Models;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.Motherboard;

public class RestoreRequest : Base.RestoreRequest, IRequest<MotherboardInfo> { }
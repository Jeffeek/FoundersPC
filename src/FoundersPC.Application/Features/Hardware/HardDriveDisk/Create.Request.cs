﻿using FoundersPC.Application.Features.Hardware.HardDriveDisk.Models;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.HardDriveDisk;

public class CreateRequest : HardDriveDiskInfo, IRequest<HardDriveDiskInfo> { }
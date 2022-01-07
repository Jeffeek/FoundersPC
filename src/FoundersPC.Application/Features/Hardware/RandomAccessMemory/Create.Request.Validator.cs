﻿using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.RandomAccessMemory;

public class CreateRequestValidator : CreateHardwareRequestValidator<CreateRequest>
{
    public CreateRequestValidator(IDbContextFactory<ApplicationDbContext> dbContextFactory) : base(dbContextFactory) { }
}
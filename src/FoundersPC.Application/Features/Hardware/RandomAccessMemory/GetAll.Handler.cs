﻿using AutoMapper;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.RandomAccessMemory.Models;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.RandomAccessMemory;

public class GetAllHandler : GetAllHardwareHandler<GetAllRequest, RandomAccessMemoryViewInfo, GetAllQuery, Domain.Entities.Hardware.RandomAccessMemory>
{
    public GetAllHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper) : base(dbContextFactory,
                                                mapper) { }
}
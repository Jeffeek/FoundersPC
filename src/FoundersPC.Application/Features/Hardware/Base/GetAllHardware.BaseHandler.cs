using AutoMapper;
using FoundersPC.Application.Features.Base;
using FoundersPC.Application.Features.Hardware.Models;
using FoundersPC.Persistence;
using FoundersPC.SharedKernel.Pagination;
using FoundersPC.SharedKernel.Query;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.Hardware.Base;

public abstract class GetAllHardwareHandler<TGetAllRequest, THardwareViewResponse, TGetAllQuery, THardware> : GetAllHandler<TGetAllRequest, THardwareViewResponse, THardware, TGetAllQuery>
    where TGetAllRequest : GetAllHardwareRequest, IRequest<IPagedList<THardwareViewResponse>>
    where THardwareViewResponse : HardwareViewInfo
    where TGetAllQuery : SortedQuery<THardware>
    where THardware : Domain.Entities.Hardware.Hardware
{
    protected GetAllHardwareHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                                    IMapper mapper) : base(dbContextFactory, mapper) { }
}
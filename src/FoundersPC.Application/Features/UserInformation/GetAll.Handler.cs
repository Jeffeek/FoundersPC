using AutoMapper;
using FoundersPC.Application.Features.Base;
using FoundersPC.Application.Features.UserInformation.Models;
using FoundersPC.Domain.Entities.Identity.Users;
using FoundersPC.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.Application.Features.UserInformation;

public class GetAllHandler : GetAllHandler<GetAllRequest, UserViewInfo, ApplicationUser, GetAllQuery>
{
    public GetAllHandler(IDbContextFactory<ApplicationDbContext> dbContextFactory,
                         IMapper mapper) : base(dbContextFactory, mapper) { }
}
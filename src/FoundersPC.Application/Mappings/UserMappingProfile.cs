using System;
using AutoMapper;
using FoundersPC.Application.Features.UserInformation.Models;
using FoundersPC.Domain.Entities.Identity.Tokens;
using FoundersPC.Domain.Entities.Identity.Users;

namespace FoundersPC.Application.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<ApplicationUser, UserInfo>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.ApplicationRole.Name));

        CreateMap<AccessToken, AccessTokenInfo>()
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => !src.IsBlocked && src.ExpirationDate > DateTime.Now))
            .ForMember(dest => dest.ActiveTo, opt => opt.MapFrom(src => src.ExpirationDate))
            .ForMember(dest => dest.ActiveFrom, opt => opt.MapFrom(src => src.StartEvaluationDate));

        CreateMap<ApplicationUser, UserViewInfo>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.ApplicationRole.Name));
    }
}
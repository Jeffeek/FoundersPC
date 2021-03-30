#region Using namespaces

using AutoMapper;
using FoundersPC.Identity.Domain.Entities.Tokens;
using FoundersPC.Identity.Domain.Entities.Users;
using FoundersPC.Identity.Dto;
using FoundersPC.RequestResponseShared.Request.ChangeSettings;
using FoundersPC.RequestResponseShared.Response.Authentication;

#endregion

namespace FoundersPC.Identity.Application.Mappings
{
    public class MappingStartup : Profile
    {
        public MappingStartup()
        {
            CreateMap<RoleEntity, RoleEntityReadDto>();
            CreateMap<UserEntity, UserEntityReadDto>();
            CreateMap<ChangeNotificationsRequest, UserNotificationsSettings>().ReverseMap();

            CreateMap<UserEntityReadDto, UserLoginResponse>()
                .ForMember(dest => dest.IsUserBlocked,
                           source => source
                               .MapFrom(x => x.IsBlocked))
                .ForMember(dest => dest.Role,
                           source => source
                               .MapFrom(x => x.Role.RoleTitle))
                .ForMember(dest => dest.IsUserActive,
                           source => source.MapFrom(x => x.IsActive))
                .ForMember(dest => dest.Email, source => source.MapFrom(x => x.Email));
        }
    }
}
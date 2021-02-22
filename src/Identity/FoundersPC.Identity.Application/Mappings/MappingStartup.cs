#region Using namespaces

using AutoMapper;
using FoundersPC.AuthorizationShared;
using FoundersPC.Identity.Application.DTO;
using FoundersPC.Identity.Domain.Entities;

#endregion

namespace FoundersPC.Identity.Application.Mappings
{
    public class MappingStartup : Profile
    {
        public MappingStartup()
        {
            CreateMap<UserEntity, UserEntityReadDto>().ReverseMap();

            CreateMap<UserEntityReadDto, UserAuthorizationResponse>()
                .ForMember(dest => dest.IsUserBlocked,
                           source => source
                               .MapFrom(x => x.IsBlocked));
        }
    }
}
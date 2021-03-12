#region Using namespaces

using AutoMapper;
using FoundersPC.Identity.Application.DTO;
using FoundersPC.Identity.Domain.Entities.Users;
using FoundersPC.RequestResponseShared.Response.Authentication;

#endregion

namespace FoundersPC.Identity.Application.Mappings
{
	public class MappingStartup : Profile
	{
		public MappingStartup()
		{
			CreateMap<RoleEntity, RoleEntityReadDto>().ReverseMap();
			CreateMap<UserEntity, UserEntityReadDto>().ReverseMap();

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
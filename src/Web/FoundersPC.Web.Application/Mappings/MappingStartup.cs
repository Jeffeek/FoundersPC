#region Using namespaces

using AutoMapper;
using FoundersPC.RequestResponseShared.Request.Authentication;
using FoundersPC.Web.Domain.Entities.ViewModels.Authentication;

#endregion

namespace FoundersPC.Web.Application.Mappings
{
    public class MappingStartup : Profile
    {
        public MappingStartup()
        {
            CreateMap<SignInViewModel, UserSignInRequest>()
                .ForMember(dest => dest.Password,
                           source => source.MapFrom(x => x.RawPassword))
                .ReverseMap();
            CreateMap<SignUpViewModel, UserSignUpRequest>()
                .ForMember(dest => dest.Password, source => source.MapFrom(x => x.RawPassword));
        }
    }
}
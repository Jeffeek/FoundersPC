#region Using namespaces

using AutoMapper;
using FoundersPC.RequestResponseShared.Request.Authentication;
using FoundersPC.RequestResponseShared.Request.ChangeSettings;
using FoundersPC.Web.Domain.Entities.ViewModels.AccountSettings;
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

            CreateMap<ForgotPasswordViewModel, UserForgotPasswordRequest>();

            CreateMap<SecuritySettingsViewModel, ChangeLoginRequest>();

            CreateMap<PasswordSettingsViewModel, ChangePasswordRequest>();

            CreateMap<NotificationsSettingsViewModel, ChangeNotificationsRequest>()
                .ForMember(dest => dest.SendMessageOnApiRequest,
                           source => source.MapFrom(x => x.SendNotificationOnUsingAPI))
                .ForMember(dest => dest.SendMessageOnEntrance,
                           source => source.MapFrom(x => x.SendNotificationOnEntrance));
        }
    }
}
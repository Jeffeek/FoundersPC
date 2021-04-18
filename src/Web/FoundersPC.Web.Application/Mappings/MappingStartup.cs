#region Using namespaces

using AutoMapper;
using FoundersPC.API.Dto;
using FoundersPC.RequestResponseShared.IdentityServer.Request.Authentication;
using FoundersPC.RequestResponseShared.IdentityServer.Request.ChangeSettings;
using FoundersPC.Web.Domain.Common.AccountSettings;
using FoundersPC.Web.Domain.Common.Authentication;
using FoundersPC.Web.Domain.Common.Hardware.PowerSupply;

#endregion

namespace FoundersPC.Web.Application.Mappings
{
    public class MappingStartup : Profile
    {
        public MappingStartup()
        {
            CreateMapForAuthenticationModels();

            CreateMapsForSettingsChangeModels();

            CreateMapsForPowerSupply();
        }

        private void CreateMapsForSettingsChangeModels()
        {
            CreateMap<SecuritySettingsViewModel, ChangeLoginRequest>();

            CreateMap<PasswordSettingsViewModel, ChangePasswordRequest>();

            CreateMap<NotificationsSettingsViewModel, ChangeNotificationsRequest>()
                .ForMember(dest => dest.SendMessageOnApiRequest,
                           source => source.MapFrom(x => x.SendNotificationOnUsingAPI))
                .ForMember(dest => dest.SendMessageOnEntrance,
                           source => source.MapFrom(x => x.SendNotificationOnEntrance));
        }

        private void CreateMapForAuthenticationModels()
        {
            CreateMap<SignInViewModel, UserSignInRequest>()
                .ForMember(dest => dest.Password,
                           source => source.MapFrom(x => x.RawPassword))
                .ReverseMap();

            CreateMap<SignUpViewModel, UserSignUpRequest>()
                .ForMember(dest => dest.Password, source => source.MapFrom(x => x.RawPassword));

            CreateMap<ForgotPasswordViewModel, UserForgotPasswordRequest>();
        }

        private void CreateMapsForPowerSupply()
        {
            CreateMap<PowerSupplyUpdateDto, PowerSupplyUpdateDtoViewModel>()
                .ForMember(dest => dest.IsCPU4PINEmpty, source => source.MapFrom(x => !x.CPU4PIN.HasValue))
                .ForMember(dest => dest.IsCPU8PINEmpty, source => source.MapFrom(x => !x.CPU8PIN.HasValue))
                .ForMember(dest => dest.Efficiency, source => source.MapFrom(x => !x.Efficiency.HasValue));

            CreateMap<PowerSupplyUpdateDtoViewModel, PowerSupplyUpdateDto>()
                .ForMember(dest => dest.CPU4PIN, source => source.MapFrom(x => x.IsCPU4PINEmpty ? null : new bool?(x.CPU4PIN)))
                .ForMember(dest => dest.CPU8PIN, source => source.MapFrom(x => x.IsCPU8PINEmpty ? null : new bool?(x.CPU8PIN)))
                .ForMember(dest => dest.Efficiency, source => source.MapFrom(x => x.IsEfficiencyEmpty ? null : new int?(x.Efficiency)));

            CreateMap<PowerSupplyInsertDto, PowerSupplyInsertDtoViewModel>()
                .ForMember(dest => dest.IsCPU4PINEmpty, source => source.MapFrom(x => !x.CPU4PIN.HasValue))
                .ForMember(dest => dest.IsCPU8PINEmpty, source => source.MapFrom(x => !x.CPU8PIN.HasValue))
                .ForMember(dest => dest.Efficiency, source => source.MapFrom(x => !x.Efficiency.HasValue));

            CreateMap<PowerSupplyInsertDtoViewModel, PowerSupplyInsertDto>()
                .ForMember(dest => dest.CPU4PIN, source => source.MapFrom(x => x.IsCPU4PINEmpty ? null : new bool?(x.CPU4PIN)))
                .ForMember(dest => dest.CPU8PIN, source => source.MapFrom(x => x.IsCPU8PINEmpty ? null : new bool?(x.CPU8PIN)))
                .ForMember(dest => dest.Efficiency, source => source.MapFrom(x => x.IsEfficiencyEmpty ? null : new int?(x.Efficiency)));
        }
    }
}
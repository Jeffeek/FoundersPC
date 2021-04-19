#region Using namespaces

using System;
using AutoMapper;
using FoundersPC.API.Dto;
using FoundersPC.RequestResponseShared.IdentityServer.Request.Authentication;
using FoundersPC.RequestResponseShared.IdentityServer.Request.ChangeSettings;
using FoundersPC.Web.Domain.Common.AccountSettings;
using FoundersPC.Web.Domain.Common.Authentication;
using FoundersPC.Web.Domain.Common.Hardware.PowerSupply;
using FoundersPC.Web.Domain.Common.Hardware.Producers;

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
            CreateMapsForProducer();
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

        private void CreateMapsForProducer()
        {
            CreateMap<ProducerInsertDtoViewModel, ProducerInsertDto>()
                .ForMember(dest => dest.FoundationDate,
                           source => source.MapFrom(x => x.IsFoundationDateEmpty
                                                             ? null
                                                             : new DateTime?(x.FoundationDate)))
                .ForMember(dest => dest.ShortName,
                           source => source.MapFrom(x => x.IsShortNameEmpty ? null : x.ShortName));

            CreateMap<ProducerInsertDto, ProducerInsertDtoViewModel>()
                .ForMember(dest => dest.IsFoundationDateEmpty,
                           source => source.MapFrom(x => x.FoundationDate == null))
                .ForMember(dest => dest.IsShortNameEmpty,
                           source => source.MapFrom(x => x.ShortName == null));

            CreateMap<ProducerUpdateDtoViewModel, ProducerUpdateDto>()
                .ForMember(dest => dest.FoundationDate,
                           source => source.MapFrom(x => x.IsFoundationDateEmpty
                                                             ? null
                                                             : new DateTime?(x.FoundationDate)))
                .ForMember(dest => dest.ShortName,
                           source => source.MapFrom(x => x.IsShortNameEmpty ? null : x.ShortName));

            CreateMap<ProducerUpdateDto, ProducerUpdateDtoViewModel>()
                .ForMember(dest => dest.IsFoundationDateEmpty,
                           source => source.MapFrom(x => x.FoundationDate == null))
                .ForMember(dest => dest.IsShortNameEmpty,
                           source => source.MapFrom(x => x.ShortName == null));
        }
    }
}
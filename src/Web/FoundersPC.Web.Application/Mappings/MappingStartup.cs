#region Using namespaces

using System;
using AutoMapper;
using FoundersPC.API.Dto;
using FoundersPC.RequestResponseShared.IdentityServer.Request.Authentication;
using FoundersPC.RequestResponseShared.IdentityServer.Request.ChangeSettings;
using FoundersPC.Web.Domain.Common.AccountSettings;
using FoundersPC.Web.Domain.Common.Authentication;
using FoundersPC.Web.Domain.Common.Hardware;

#endregion

namespace FoundersPC.Web.Application.Mappings
{
    public class MappingStartup : Profile
    {
        public MappingStartup()
        {
            CreateMapForAuthenticationModels();
            CreateMapsForSettingsChangeModels();

            CreateMapsForCases();
            CreateMapsForHardDriveDisk();
            CreateMapsForMotherboard();
            CreateMapsForProcessor();
            CreateMapsForProcessorCore();
            CreateMapsForRandomAccessMemory();
            CreateMapsForSolidStateDrive();
            CreateMapsForVideoCard();
            CreateMapsForVideoCardCore();
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

        #region Hardware mapping

        private void CreateMapsForCases()
        {
            CreateMap<CaseInsertDto, CaseDtoViewModel>()
                .ForMember(dest => dest.IsDepthEmpty, source => source.MapFrom(x => x.Depth == null))
                .ForMember(dest => dest.IsHeightEmpty, source => source.MapFrom(x => x.Height == null))
                .ForMember(dest => dest.IsWeightEmpty, source => source.MapFrom(x => x.Weight == null))
                .ForMember(dest => dest.IsWidthEmpty, source => source.MapFrom(x => x.Width == null));

            CreateMap<CaseDtoViewModel, CaseInsertDto>()
                .ForMember(dest => dest.Depth, source => source.MapFrom(x => x.IsDepthEmpty ? null : new int?(x.Depth)))
                .ForMember(dest => dest.Height, source => source.MapFrom(x => x.IsHeightEmpty ? null : new int?(x.Height)))
                .ForMember(dest => dest.Weight, source => source.MapFrom(x => x.IsWeightEmpty ? null : new double?(x.Weight)))
                .ForMember(dest => dest.Width, source => source.MapFrom(x => x.IsWidthEmpty ? null : new int?(x.Width)));

            CreateMap<CaseUpdateDto, CaseDtoViewModel>()
                .ForMember(dest => dest.IsDepthEmpty, source => source.MapFrom(x => x.Depth == null))
                .ForMember(dest => dest.IsHeightEmpty, source => source.MapFrom(x => x.Height == null))
                .ForMember(dest => dest.IsWeightEmpty, source => source.MapFrom(x => x.Weight == null))
                .ForMember(dest => dest.IsWidthEmpty, source => source.MapFrom(x => x.Width == null));

            CreateMap<CaseDtoViewModel, CaseUpdateDto>()
                .ForMember(dest => dest.Depth, source => source.MapFrom(x => x.IsDepthEmpty ? null : new int?(x.Depth)))
                .ForMember(dest => dest.Height, source => source.MapFrom(x => x.IsHeightEmpty ? null : new int?(x.Height)))
                .ForMember(dest => dest.Weight, source => source.MapFrom(x => x.IsWeightEmpty ? null : new double?(x.Weight)))
                .ForMember(dest => dest.Width, source => source.MapFrom(x => x.IsWidthEmpty ? null : new int?(x.Width)));
        }

        private void CreateMapsForHardDriveDisk()
        {
            CreateMap<HardDriveDiskInsertDto, HardDriveDiskDtoViewModel>()
                .ReverseMap();

            CreateMap<HardDriveDiskUpdateDto, HardDriveDiskDtoViewModel>()
                .ReverseMap();
        }

        private void CreateMapsForMotherboard()
        {
            CreateMap<MotherboardInsertDto, MotherboardDtoViewModel>()
                .ReverseMap();

            CreateMap<MotherboardUpdateDto, MotherboardDtoViewModel>()
                .ReverseMap();
        }

        private void CreateMapsForProcessor()
        {
            CreateMap<ProcessorInsertDto, ProcessorDtoViewModel>()
                .ReverseMap();

            CreateMap<ProcessorUpdateDto, ProcessorDtoViewModel>()
                .ReverseMap();
        }

        private void CreateMapsForProcessorCore()
        {
            CreateMap<ProcessorCoreInsertDto, ProcessorCoreDtoViewModel>()
                .ForMember(dest => dest.IsMarketLaunchEmpty,
                           source => source.MapFrom(x => x.MarketLaunch == null));

            CreateMap<ProcessorCoreDtoViewModel, ProcessorCoreInsertDto>()
                .ForMember(dest => dest.MarketLaunch,
                           source => source.MapFrom(x => x.IsMarketLaunchEmpty ? null : new DateTime?(x.MarketLaunch)));

            CreateMap<ProcessorCoreUpdateDto, ProcessorCoreDtoViewModel>()
                .ForMember(dest => dest.IsMarketLaunchEmpty,
                           source => source.MapFrom(x => x.MarketLaunch == null));

            CreateMap<ProcessorCoreDtoViewModel, ProcessorCoreUpdateDto>()
                .ForMember(dest => dest.MarketLaunch,
                           source => source.MapFrom(x => x.IsMarketLaunchEmpty ? null : new DateTime?(x.MarketLaunch)));
        }

        private void CreateMapsForRandomAccessMemory()
        {
            CreateMap<RandomAccessMemoryInsertDto, RandomAccessMemoryDtoViewModel>()
                .ReverseMap();

            CreateMap<RandomAccessMemoryUpdateDto, RandomAccessMemoryDtoViewModel>()
                .ReverseMap();
        }

        private void CreateMapsForSolidStateDrive()
        {
            CreateMap<SolidStateDriveInsertDto, SolidStateDriveDtoViewModel>()
                .ReverseMap();

            CreateMap<SolidStateDriveUpdateDto, SolidStateDriveDtoViewModel>()
                .ReverseMap();
        }

        private void CreateMapsForVideoCard()
        {
            CreateMap<VideoCardInsertDto, VideoCardDtoViewModel>()
                .ReverseMap();

            CreateMap<VideoCardUpdateDto, VideoCardDtoViewModel>()
                .ReverseMap();
        }

        private void CreateMapsForVideoCardCore()
        {
            CreateMap<VideoCardCoreInsertDto, VideoCardCoreDtoViewModel>()
                .ReverseMap();

            CreateMap<VideoCardCoreUpdateDto, VideoCardCoreDtoViewModel>()
                .ReverseMap();
        }

        private void CreateMapsForProducer()
        {
            CreateMap<ProducerDtoViewModel, ProducerInsertDto>()
                .ForMember(dest => dest.FoundationDate,
                           source => source.MapFrom(x => x.IsFoundationDateEmpty
                                                             ? null
                                                             : new DateTime?(x.FoundationDate)))
                .ForMember(dest => dest.ShortName,
                           source => source.MapFrom(x => x.IsShortNameEmpty ? null : x.ShortName))
                .ForMember(dest => dest.Website, source => source.MapFrom(x => x.IsWebsiteEmpty ? null : x.Website));

            CreateMap<ProducerInsertDto, ProducerDtoViewModel>()
                .ForMember(dest => dest.IsFoundationDateEmpty,
                           source => source.MapFrom(x => x.FoundationDate == null))
                .ForMember(dest => dest.IsShortNameEmpty,
                           source => source.MapFrom(x => x.ShortName == null))
                .ForMember(dest => dest.IsWebsiteEmpty, source => source.MapFrom(x => x.Website == null));

            CreateMap<ProducerDtoViewModel, ProducerUpdateDto>()
                .ForMember(dest => dest.FoundationDate,
                           source => source.MapFrom(x => x.IsFoundationDateEmpty
                                                             ? null
                                                             : new DateTime?(x.FoundationDate)))
                .ForMember(dest => dest.ShortName,
                           source => source.MapFrom(x => x.IsShortNameEmpty ? null : x.ShortName))
                .ForMember(dest => dest.Website, source => source.MapFrom(x => x.IsWebsiteEmpty ? null : x.Website));

            CreateMap<ProducerUpdateDto, ProducerDtoViewModel>()
                .ForMember(dest => dest.IsFoundationDateEmpty,
                           source => source.MapFrom(x => x.FoundationDate == null))
                .ForMember(dest => dest.IsShortNameEmpty,
                           source => source.MapFrom(x => x.ShortName == null))
                .ForMember(dest => dest.IsWebsiteEmpty, source => source.MapFrom(x => x.Website == null));
        }

        private void CreateMapsForPowerSupply()
        {
            CreateMap<PowerSupplyUpdateDto, PowerSupplyDtoViewModel>()
                .ForMember(dest => dest.IsCPU4PINEmpty, source => source.MapFrom(x => !x.CPU4PIN.HasValue))
                .ForMember(dest => dest.IsCPU8PINEmpty, source => source.MapFrom(x => !x.CPU8PIN.HasValue))
                .ForMember(dest => dest.Efficiency, source => source.MapFrom(x => !x.Efficiency.HasValue));

            CreateMap<PowerSupplyDtoViewModel, PowerSupplyUpdateDto>()
                .ForMember(dest => dest.CPU4PIN, source => source.MapFrom(x => x.IsCPU4PINEmpty ? null : new bool?(x.CPU4PIN)))
                .ForMember(dest => dest.CPU8PIN, source => source.MapFrom(x => x.IsCPU8PINEmpty ? null : new bool?(x.CPU8PIN)))
                .ForMember(dest => dest.Efficiency, source => source.MapFrom(x => x.IsEfficiencyEmpty ? null : new int?(x.Efficiency)));

            CreateMap<PowerSupplyInsertDto, PowerSupplyDtoViewModel>()
                .ForMember(dest => dest.IsCPU4PINEmpty, source => source.MapFrom(x => !x.CPU4PIN.HasValue))
                .ForMember(dest => dest.IsCPU8PINEmpty, source => source.MapFrom(x => !x.CPU8PIN.HasValue))
                .ForMember(dest => dest.Efficiency, source => source.MapFrom(x => !x.Efficiency.HasValue));

            CreateMap<PowerSupplyDtoViewModel, PowerSupplyInsertDto>()
                .ForMember(dest => dest.CPU4PIN, source => source.MapFrom(x => x.IsCPU4PINEmpty ? null : new bool?(x.CPU4PIN)))
                .ForMember(dest => dest.CPU8PIN, source => source.MapFrom(x => x.IsCPU8PINEmpty ? null : new bool?(x.CPU8PIN)))
                .ForMember(dest => dest.Efficiency, source => source.MapFrom(x => x.IsEfficiencyEmpty ? null : new int?(x.Efficiency)));
        }

        #endregion
    }
}
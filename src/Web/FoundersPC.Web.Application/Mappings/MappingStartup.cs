#region Using namespaces

using System;
using AutoMapper;
using FoundersPC.API.Dto;
using FoundersPC.RequestResponseShared.IdentityServer.Request.Authentication;
using FoundersPC.RequestResponseShared.IdentityServer.Request.ChangeSettings;
using FoundersPC.Web.Domain.Common.AccountSettings;
using FoundersPC.Web.Domain.Common.Authentication;
using FoundersPC.Web.Domain.Common.Hardware.Cases;
using FoundersPC.Web.Domain.Common.Hardware.HardDriveDisk;
using FoundersPC.Web.Domain.Common.Hardware.Motherboard;
using FoundersPC.Web.Domain.Common.Hardware.PowerSupply;
using FoundersPC.Web.Domain.Common.Hardware.Processor;
using FoundersPC.Web.Domain.Common.Hardware.ProcessorCore;
using FoundersPC.Web.Domain.Common.Hardware.Producer;
using FoundersPC.Web.Domain.Common.Hardware.RandomAccessMemory;
using FoundersPC.Web.Domain.Common.Hardware.SolidStateDrive;
using FoundersPC.Web.Domain.Common.Hardware.VideoCard;
using FoundersPC.Web.Domain.Common.Hardware.VideoCardCore;

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
            CreateMap<CaseInsertDto, CaseInsertDtoViewModel>()
                .ForMember(dest => dest.IsDepthEmpty, source => source.MapFrom(x => x.Depth == null))
                .ForMember(dest => dest.IsHeightEmpty, source => source.MapFrom(x => x.Height == null))
                .ForMember(dest => dest.IsWeightEmpty, source => source.MapFrom(x => x.Weight == null))
                .ForMember(dest => dest.IsWidthEmpty, source => source.MapFrom(x => x.Width == null));

            CreateMap<CaseInsertDtoViewModel, CaseInsertDto>()
                .ForMember(dest => dest.Depth, source => source.MapFrom(x => x.IsDepthEmpty ? null : new int?(x.Depth)))
                .ForMember(dest => dest.Height, source => source.MapFrom(x => x.IsHeightEmpty ? null : new int?(x.Height)))
                .ForMember(dest => dest.Weight, source => source.MapFrom(x => x.IsWeightEmpty ? null : new double?(x.Weight)))
                .ForMember(dest => dest.Width, source => source.MapFrom(x => x.IsWidthEmpty ? null : new int?(x.Width)));

            CreateMap<CaseUpdateDto, CaseUpdateDtoViewModel>()
                .ForMember(dest => dest.IsDepthEmpty, source => source.MapFrom(x => x.Depth == null))
                .ForMember(dest => dest.IsHeightEmpty, source => source.MapFrom(x => x.Height == null))
                .ForMember(dest => dest.IsWeightEmpty, source => source.MapFrom(x => x.Weight == null))
                .ForMember(dest => dest.IsWidthEmpty, source => source.MapFrom(x => x.Width == null));

            CreateMap<CaseUpdateDtoViewModel, CaseUpdateDto>()
                .ForMember(dest => dest.Depth, source => source.MapFrom(x => x.IsDepthEmpty ? null : new int?(x.Depth)))
                .ForMember(dest => dest.Height, source => source.MapFrom(x => x.IsHeightEmpty ? null : new int?(x.Height)))
                .ForMember(dest => dest.Weight, source => source.MapFrom(x => x.IsWeightEmpty ? null : new double?(x.Weight)))
                .ForMember(dest => dest.Width, source => source.MapFrom(x => x.IsWidthEmpty ? null : new int?(x.Width)));
        }

        private void CreateMapsForHardDriveDisk()
        {
            CreateMap<HardDriveDiskInsertDto, HardDriveDiskInsertDtoViewModel>()
                .ReverseMap();

            CreateMap<HardDriveDiskUpdateDto, HardDriveDiskUpdateDtoViewModel>()
                .ReverseMap();
        }

        private void CreateMapsForMotherboard()
        {
            CreateMap<MotherboardInsertDto, MotherboardInsertDtoViewModel>()
                .ReverseMap();

            CreateMap<MotherboardUpdateDto, MotherboardUpdateDtoViewModel>()
                .ReverseMap();
        }

        private void CreateMapsForProcessor()
        {
            CreateMap<ProcessorInsertDto, ProcessorInsertDtoViewModel>()
                .ReverseMap();

            CreateMap<ProcessorUpdateDto, ProcessorUpdateDtoViewModel>()
                .ReverseMap();
        }

        private void CreateMapsForProcessorCore()
        {
            CreateMap<ProcessorCoreInsertDto, ProcessorCoreInsertDtoViewModel>()
                .ForMember(dest => dest.IsMarketLaunchEmpty,
                           source => source.MapFrom(x => x.MarketLaunch == null));

            CreateMap<ProcessorCoreInsertDtoViewModel, ProcessorCoreInsertDto>()
                .ForMember(dest => dest.MarketLaunch,
                           source => source.MapFrom(x => x.IsMarketLaunchEmpty ? null : new DateTime?(x.MarketLaunch)));

            CreateMap<ProcessorCoreUpdateDto, ProcessorCoreUpdateDtoViewModel>()
                .ForMember(dest => dest.IsMarketLaunchEmpty,
                           source => source.MapFrom(x => x.MarketLaunch == null));

            CreateMap<ProcessorCoreUpdateDtoViewModel, ProcessorCoreUpdateDto>()
                .ForMember(dest => dest.MarketLaunch,
                           source => source.MapFrom(x => x.IsMarketLaunchEmpty ? null : new DateTime?(x.MarketLaunch)));
        }

        private void CreateMapsForRandomAccessMemory()
        {
            CreateMap<RandomAccessMemoryInsertDto, RandomAccessMemoryInsertDtoViewModel>()
                .ReverseMap();

            CreateMap<RandomAccessMemoryUpdateDto, RandomAccessMemoryUpdateDtoViewModel>()
                .ReverseMap();
        }

        private void CreateMapsForSolidStateDrive()
        {
            CreateMap<SolidStateDriveInsertDto, SolidStateDriveInsertDtoViewModel>()
                .ReverseMap();

            CreateMap<SolidStateDriveUpdateDto, SolidStateDriveUpdateDtoViewModel>()
                .ReverseMap();
        }

        private void CreateMapsForVideoCard()
        {
            CreateMap<VideoCardInsertDto, VideoCardInsertDtoViewModel>()
                .ReverseMap();

            CreateMap<VideoCardUpdateDto, VideoCardUpdateDtoViewModel>()
                .ReverseMap();
        }

        private void CreateMapsForVideoCardCore()
        {
            CreateMap<VideoCardCoreInsertDto, VideoCardCoreInsertDtoViewModel>()
                .ReverseMap();

            CreateMap<VideoCardCoreUpdateDto, VideoCardCoreUpdateDtoViewModel>()
                .ReverseMap();
        }

        private void CreateMapsForProducer()
        {
            CreateMap<ProducerInsertDtoViewModel, ProducerInsertDto>()
                .ForMember(dest => dest.FoundationDate,
                           source => source.MapFrom(x => x.IsFoundationDateEmpty
                                                             ? null
                                                             : new DateTime?(x.FoundationDate)))
                .ForMember(dest => dest.ShortName,
                           source => source.MapFrom(x => x.IsShortNameEmpty ? null : x.ShortName))
                .ForMember(dest => dest.Website, source => source.MapFrom(x => x.IsWebsiteEmpty ? null : x.Website));

            CreateMap<ProducerInsertDto, ProducerInsertDtoViewModel>()
                .ForMember(dest => dest.IsFoundationDateEmpty,
                           source => source.MapFrom(x => x.FoundationDate == null))
                .ForMember(dest => dest.IsShortNameEmpty,
                           source => source.MapFrom(x => x.ShortName == null))
                .ForMember(dest => dest.IsWebsiteEmpty, source => source.MapFrom(x => x.Website == null));

            CreateMap<ProducerUpdateDtoViewModel, ProducerUpdateDto>()
                .ForMember(dest => dest.FoundationDate,
                           source => source.MapFrom(x => x.IsFoundationDateEmpty
                                                             ? null
                                                             : new DateTime?(x.FoundationDate)))
                .ForMember(dest => dest.ShortName,
                           source => source.MapFrom(x => x.IsShortNameEmpty ? null : x.ShortName))
                .ForMember(dest => dest.Website, source => source.MapFrom(x => x.IsWebsiteEmpty ? null : x.Website));

            CreateMap<ProducerUpdateDto, ProducerUpdateDtoViewModel>()
                .ForMember(dest => dest.IsFoundationDateEmpty,
                           source => source.MapFrom(x => x.FoundationDate == null))
                .ForMember(dest => dest.IsShortNameEmpty,
                           source => source.MapFrom(x => x.ShortName == null))
                .ForMember(dest => dest.IsWebsiteEmpty, source => source.MapFrom(x => x.Website == null));
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

        #endregion
    }
}
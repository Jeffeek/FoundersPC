using AutoMapper;
using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.Case.Models;
using FoundersPC.Application.Features.Hardware.HardDriveDisk.Models;
using FoundersPC.Application.Features.Hardware.Motherboard.Models;
using FoundersPC.Application.Features.Hardware.PowerSupply.Models;
using FoundersPC.Application.Features.Hardware.Processor.Models;
using FoundersPC.Application.Features.Hardware.RandomAccessMemory.Models;
using FoundersPC.Application.Features.Hardware.SolidStateDrive.Models;
using FoundersPC.Application.Features.Hardware.VideoCard.Models;
using FoundersPC.Application.Features.Producer.Models;
using FoundersPC.Application.Features.UserInformation.Models;
using FoundersPC.SharedKernel.Extensions;
using FoundersPC.SharedKernel.Filter;
using FoundersPC.SharedKernel.Pagination;
using FoundersPC.UI.Admin.Locators;
using FoundersPC.UI.Admin.Models;

namespace FoundersPC.UI.Admin.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region FilterOptions

        CreateMap<FilterOptions, SortedPagedFilter>()
            .IncludeAllDerived()
            .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.Pagination.CurrentPageSize))
            .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => src.Pagination.CurrentPage))
            .ForMember(dest => dest.SortColumn, opt => opt.MapFrom(src => src.OrderSettings.SortColumn.RemoveSpaces()));

        CreateMap<FilterOptions, GetAllHardwareRequest>()
            .IncludeAllDerived()
            .ForMember(dest => dest.IsAscending, opt => opt.MapFrom(src => src.OrderSettings.IsAscending));

        CreateMap<FilterOptions, Application.Features.Hardware.Case.GetAllRequest>();
        CreateMap<FilterOptions, Application.Features.Hardware.HardDriveDisk.GetAllRequest>();
        CreateMap<FilterOptions, Application.Features.Hardware.Motherboard.GetAllRequest>();
        CreateMap<FilterOptions, Application.Features.Hardware.PowerSupply.GetAllRequest>();
        CreateMap<FilterOptions, Application.Features.Hardware.Processor.GetAllRequest>();
        CreateMap<FilterOptions, Application.Features.Hardware.RandomAccessMemory.GetAllRequest>();
        CreateMap<FilterOptions, Application.Features.Hardware.SolidStateDrive.GetAllRequest>();
        CreateMap<FilterOptions, Application.Features.Hardware.VideoCard.GetAllRequest>();
        CreateMap<FilterOptions, Application.Features.Producer.GetAllRequest>()
            .ForMember(dest => dest.IsAscending, opt => opt.MapFrom(src => src.OrderSettings.IsAscending));
        CreateMap<FilterOptions, Application.Features.UserInformation.GetAllRequest>()
            .ForMember(dest => dest.ShowBlocked, opt => opt.MapFrom(src => src.ShowDeleted))
            .ForMember(dest => dest.IsAscending, opt => opt.MapFrom(src => src.OrderSettings.IsAscending));

        #endregion

        #region Pagination

        CreateMap<PagingInfo, Pagination>()
            .ForMember(dest => dest.CurrentPage, opt => opt.MapFrom(src => src.PageNumber))
            .ForMember(dest => dest.IsMoveBackAvailable, opt => opt.MapFrom(src => src.PageNumber != 0))
            .ForMember(dest => dest.IsMoveStraightAvailable, opt => opt.MapFrom(src => src.PageCount > src.PageNumber + 1))
            .ForMember(dest => dest.CurrentPageSize, opt => opt.MapFrom(src => src.PageSize));

        #endregion

        #region Info -> InfoViewModel

        CreateMap<CaseInfo, CaseInfoViewModel>();
        CreateMap<HardDriveDiskInfo, HardDriveDiskInfoViewModel>();
        CreateMap<MotherboardInfo, MotherboardInfoViewModel>();
        CreateMap<PowerSupplyInfo, PowerSupplyInfoViewModel>();
        CreateMap<ProcessorInfo, ProcessorInfoViewModel>();
        CreateMap<RandomAccessMemoryInfo, RandomAccessMemoryInfoViewModel>();
        CreateMap<SolidStateDriveInfo, SolidStateDriveInfoViewModel>();
        CreateMap<VideoCardInfo, VideoCardInfoViewModel>();
        CreateMap<ProducerInfo, ProducerInfoViewModel>();
        CreateMap<UserInfo, UserInfoViewModel>();
        CreateMap<AccessTokenInfo, AccessTokenViewModel>();

        #endregion

        #region InfoViewModel -> Info

        CreateMap<CaseInfoViewModel, CaseInfo>()
            .IncludeAllDerived()
            .ForMember(dest => dest.CaseTypeId, opt => opt.MapFrom(src => src.CaseTypeId < 0 ? null : src.CaseTypeId))
            .ForMember(dest => dest.WindowMaterialId, opt => opt.MapFrom(src => src.WindowMaterialId < 0 ? null : src.WindowMaterialId))
            .ForMember(dest => dest.MaterialId, opt => opt.MapFrom(src => src.MaterialId < 0 ? null : src.MaterialId))
            .ForMember(dest => dest.ColorId, opt => opt.MapFrom(src => src.ColorId < 0 ? null : src.ColorId));

        CreateMap<HardDriveDiskInfoViewModel, HardDriveDiskInfo>()
            .IncludeAllDerived()
            .ForMember(dest => dest.DiskConnectionInterfaceId, opt => opt.MapFrom(src => src.DiskConnectionInterfaceId < 0 ? null : src.DiskConnectionInterfaceId))
            .ForMember(dest => dest.DiskFactorId, opt => opt.MapFrom(src => src.DiskFactorId < 0 ? null : src.DiskFactorId));

        CreateMap<MotherboardInfoViewModel, MotherboardInfo>()
            .IncludeAllDerived()
            .ForMember(dest => dest.MotherboardFactorId, opt => opt.MapFrom(src => src.MotherboardFactorId < 0 ? null : src.MotherboardFactorId))
            .ForMember(dest => dest.RAMModeId, opt => opt.MapFrom(src => src.RAMModeId < 0 ? null : src.RAMModeId))
            .ForMember(dest => dest.RAMTypeId, opt => opt.MapFrom(src => src.RAMTypeId < 0 ? null : src.RAMTypeId))
            .ForMember(dest => dest.SocketId, opt => opt.MapFrom(src => src.SocketId < 0 ? null : src.SocketId));

        CreateMap<PowerSupplyInfoViewModel, PowerSupplyInfo>()
            .IncludeAllDerived()
            .ForMember(dest => dest.MotherboardPoweringId, opt => opt.MapFrom(src => src.MotherboardPoweringId < 0 ? null : src.MotherboardPoweringId));

        CreateMap<ProcessorInfoViewModel, ProcessorInfo>()
            .IncludeAllDerived()
            .ForMember(dest => dest.SocketId, opt => opt.MapFrom(src => src.SocketId < 0 ? null : src.SocketId))
            .ForMember(dest => dest.IntegratedGraphicsId, opt => opt.MapFrom(src => src.IntegratedGraphicsId < 0 ? null : src.IntegratedGraphicsId))
            .ForMember(dest => dest.TechProcessId, opt => opt.MapFrom(src => src.TechProcessId < 0 ? null : src.TechProcessId));

        CreateMap<RandomAccessMemoryInfoViewModel, RandomAccessMemoryInfo>()
            .IncludeAllDerived()
            .ForMember(dest => dest.RAMTypeId, opt => opt.MapFrom(src => src.RAMTypeId < 0 ? null : src.RAMTypeId));

        CreateMap<SolidStateDriveInfoViewModel, SolidStateDriveInfo>()
            .IncludeAllDerived()
            .ForMember(dest => dest.DiskConnectionInterfaceId, opt => opt.MapFrom(src => src.DiskConnectionInterfaceId < 0 ? null : src.DiskConnectionInterfaceId))
            .ForMember(dest => dest.DiskFactorId, opt => opt.MapFrom(src => src.DiskFactorId < 0 ? null : src.DiskFactorId));

        CreateMap<VideoCardInfoViewModel, VideoCardInfo>()
            .IncludeAllDerived()
            .ForMember(dest => dest.VideoMemoryTypeId, opt => opt.MapFrom(src => src.VideoMemoryTypeId < 0 ? null : src.VideoMemoryTypeId));

        CreateMap<ProducerInfoViewModel, ProducerInfo>()
            .IncludeAllDerived()
            .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId < 0 ? null : src.CountryId));

        #endregion

        #region InfoViewModel -> Create/Update Request

        CreateMap<CaseInfoViewModel, Application.Features.Hardware.Case.CreateRequest>();
        CreateMap<CaseInfoViewModel, Application.Features.Hardware.Case.UpdateRequest>();

        CreateMap<HardDriveDiskInfoViewModel, Application.Features.Hardware.HardDriveDisk.CreateRequest>();
        CreateMap<HardDriveDiskInfoViewModel, Application.Features.Hardware.HardDriveDisk.UpdateRequest>();

        CreateMap<MotherboardInfoViewModel, Application.Features.Hardware.Motherboard.CreateRequest>();
        CreateMap<MotherboardInfoViewModel, Application.Features.Hardware.Motherboard.UpdateRequest>();

        CreateMap<PowerSupplyInfoViewModel, Application.Features.Hardware.PowerSupply.CreateRequest>();
        CreateMap<PowerSupplyInfoViewModel, Application.Features.Hardware.PowerSupply.UpdateRequest>();

        CreateMap<ProcessorInfoViewModel, Application.Features.Hardware.Processor.CreateRequest>();
        CreateMap<ProcessorInfoViewModel, Application.Features.Hardware.Processor.UpdateRequest>();

        CreateMap<RandomAccessMemoryInfoViewModel, Application.Features.Hardware.RandomAccessMemory.CreateRequest>();
        CreateMap<RandomAccessMemoryInfoViewModel, Application.Features.Hardware.RandomAccessMemory.UpdateRequest>();

        CreateMap<SolidStateDriveInfoViewModel, Application.Features.Hardware.SolidStateDrive.CreateRequest>();
        CreateMap<SolidStateDriveInfoViewModel, Application.Features.Hardware.SolidStateDrive.UpdateRequest>();

        CreateMap<VideoCardInfoViewModel, Application.Features.Hardware.VideoCard.CreateRequest>();
        CreateMap<VideoCardInfoViewModel, Application.Features.Hardware.VideoCard.UpdateRequest>();

        CreateMap<ProducerInfoViewModel, Application.Features.Producer.CreateRequest>();
        CreateMap<ProducerInfoViewModel, Application.Features.Producer.UpdateRequest>();

        #endregion
    }
}
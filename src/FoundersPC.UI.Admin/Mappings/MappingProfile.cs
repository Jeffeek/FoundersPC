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
using FoundersPC.SharedKernel.Extensions;
using FoundersPC.SharedKernel.Pagination;
using FoundersPC.UI.Admin.Locators;
using FoundersPC.UI.Admin.Models;

namespace FoundersPC.UI.Admin.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<FilterOptions, GetAllHardwareRequest>()
            .IncludeAllDerived()
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.ShowDeleted))
            .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => src.Pagination.CurrentPage))
            .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.Pagination.CurrentPageSize))
            .ForMember(dest => dest.SortColumn, opt => opt.MapFrom(src => src.OrderSettings.SortColumn.RemoveSpaces()))
            .ForMember(dest => dest.SearchText, opt => opt.MapFrom(src => src.SearchText));

        CreateMap<FilterOptions, Application.Features.Hardware.Case.GetAllRequest>();

        CreateMap<PagingInfo, Pagination>()
            .ForMember(dest => dest.CurrentPage, opt => opt.MapFrom(src => src.PageNumber))
            .ForMember(dest => dest.IsMoveBackAvailable, opt => opt.MapFrom(src => src.PageNumber != 0))
            .ForMember(dest => dest.IsMoveStraightAvailable, opt => opt.MapFrom(src => src.PageCount > src.PageNumber + 1))
            .ForMember(dest => dest.CurrentPageSize, opt => opt.MapFrom(src => src.PageSize));

        CreateMap<CaseInfo, CaseInfoViewModel>();

        CreateMap<CaseInfoViewModel, CaseInfo>()
            .IncludeAllDerived()
            .ForMember(dest => dest.CaseTypeId, opt => opt.MapFrom(src => src.CaseTypeId < 0 ? null : src.CaseTypeId))
            .ForMember(dest => dest.WindowMaterialId, opt => opt.MapFrom(src => src.WindowMaterialId < 0 ? null : src.WindowMaterialId))
            .ForMember(dest => dest.MaterialId, opt => opt.MapFrom(src => src.MaterialId < 0 ? null : src.MaterialId))
            .ForMember(dest => dest.ColorId, opt => opt.MapFrom(src => src.ColorId < 0 ? null : src.ColorId));

        CreateMap<CaseInfoViewModel, Application.Features.Hardware.Case.CreateRequest>();
        CreateMap<CaseInfoViewModel, Application.Features.Hardware.Case.UpdateRequest>();
    }
}
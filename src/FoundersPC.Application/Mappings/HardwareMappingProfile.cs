using AutoMapper;
using FoundersPC.Application.Features.Hardware;
using FoundersPC.Application.Features.Hardware.Models;
using FoundersPC.Domain.Entities.Hardware;
using FoundersPC.Domain.Entities.Hardware.Metadata;

namespace FoundersPC.Application.Mappings;

public class HardwareMappingProfile : Profile
{
    public HardwareMappingProfile()
    {
        CreateMap<GetHardwareRequest, Features.Hardware.Case.GetRequest>();
        CreateMap<GetHardwareRequest, Features.Hardware.HardDriveDisk.GetRequest>();
        CreateMap<GetHardwareRequest, Features.Hardware.Motherboard.GetRequest>();
        CreateMap<GetHardwareRequest, Features.Hardware.PowerSupply.GetRequest>();
        CreateMap<GetHardwareRequest, Features.Hardware.Processor.GetRequest>();
        CreateMap<GetHardwareRequest, Features.Hardware.RandomAccessMemory.GetRequest>();
        CreateMap<GetHardwareRequest, Features.Hardware.SolidStateDrive.GetRequest>();
        CreateMap<GetHardwareRequest, Features.Hardware.VideoCard.GetRequest>();

        CreateMap<Hardware, HardwareInfo>()
            .IncludeAllDerived()
            .IncludeMembers(src => src.BaseMetadata)
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.Login))
            .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(src => src.LastModifiedBy.Login));

        CreateMap<HardwareMetadata, HardwareInfo>()
            .IncludeAllDerived();

        CreateMap<Producer, ProducerInfo>()
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.Login))
            .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(src => src.LastModifiedBy.Login));
    }
}
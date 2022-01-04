using AutoMapper;
using FoundersPC.Application.Features.Hardware;
using FoundersPC.Application.Features.Hardware.Case.Models;
using FoundersPC.Application.Features.Hardware.HardDriveDisk.Models;
using FoundersPC.Application.Features.Hardware.Models;
using FoundersPC.Application.Features.Hardware.Motherboard.Models;
using FoundersPC.Application.Features.Hardware.PowerSupply.Models;
using FoundersPC.Application.Features.Hardware.Processor.Models;
using FoundersPC.Application.Features.Hardware.RandomAccessMemory.Models;
using FoundersPC.Application.Features.Hardware.SolidStateDrive.Models;
using FoundersPC.Application.Features.Hardware.VideoCard.Models;
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

        CreateMap<Case, CaseInfo>()
            .IncludeMembers(x => x.Metadata);

        CreateMap<CaseMetadata, CaseInfo>();

        CreateMap<HardDriveDisk, HardDriveDiskInfo>()
            .IncludeMembers(x => x.Metadata);

        CreateMap<HardDriveDiskMetadata, HardDriveDiskInfo>();

        CreateMap<Motherboard, MotherboardInfo>()
            .IncludeMembers(x => x.Metadata);

        CreateMap<MotherboardMetadata, MotherboardInfo>();

        CreateMap<PowerSupply, PowerSupplyInfo>()
            .IncludeMembers(x => x.Metadata);

        CreateMap<PowerSupplyMetadata, PowerSupplyInfo>();

        CreateMap<Processor, ProcessorInfo>()
            .IncludeMembers(x => x.Metadata);

        CreateMap<ProcessorMetadata, ProcessorInfo>();

        CreateMap<RandomAccessMemory, RandomAccessMemoryInfo>()
            .IncludeMembers(x => x.Metadata);

        CreateMap<RandomAccessMemoryMetadata, RandomAccessMemoryInfo>();

        CreateMap<SolidStateDrive, SolidStateDriveInfo>()
            .IncludeMembers(x => x.Metadata);

        CreateMap<SolidStateDriveMetadata, SolidStateDriveInfo>();

        CreateMap<VideoCard, VideoCardInfo>()
            .IncludeMembers(x => x.Metadata);

        CreateMap<VideoCardMetadata, VideoCardInfo>();
    }
}
using AutoMapper;
using FoundersPC.Application.Features.Client.Hardware.Case.Models;
using FoundersPC.Application.Features.Client.Hardware.HardDriveDisk.Models;
using FoundersPC.Application.Features.Client.Hardware.Models;
using FoundersPC.Application.Features.Client.Hardware.Motherboard.Models;
using FoundersPC.Application.Features.Client.Hardware.PowerSupply.Models;
using FoundersPC.Application.Features.Client.Hardware.Processor.Models;
using FoundersPC.Application.Features.Client.Hardware.RandomAccessMemory.Models;
using FoundersPC.Application.Features.Client.Hardware.SolidStateDrive.Models;
using FoundersPC.Application.Features.Client.Hardware.VideoCard.Models;
using FoundersPC.Application.Features.Client.Producer.Models;
using FoundersPC.Domain.Entities.Hardware;

namespace FoundersPC.Application.Mappings;

public class HardwareToClientHardwareMapping : Profile
{
    public HardwareToClientHardwareMapping()
    {
        CreateMap<Hardware, ClientHardwareInfo>()
            .IncludeAllDerived();

        CreateMap<Producer, ClientProducerInfo>()
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country!.Name));

        CreateMap<Case, ClientCaseInfo>()
            .ForMember(dest => dest.CaseType, opt => opt.MapFrom(src => src.Metadata.CaseType!.Name))
            .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Metadata.Color!.Name))
            .ForMember(dest => dest.Material, opt => opt.MapFrom(src => src.Metadata.Material!.Name))
            .ForMember(dest => dest.WindowMaterial, opt => opt.MapFrom(src => src.Metadata.WindowMaterial!.Name))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Metadata.Title));

        CreateMap<HardDriveDisk, ClientHardDriveDiskInfo>()
            .ForMember(dest => dest.DiskConnectionInterface, opt => opt.MapFrom(src => src.Metadata.DiskConnectionInterface!.Name))
            .ForMember(dest => dest.DiskFactor, opt => opt.MapFrom(src => src.Metadata.Factor!.Name));

        CreateMap<Motherboard, ClientMotherboardInfo>()
            .ForMember(dest => dest.RAMType, opt => opt.MapFrom(src => src.Metadata.RAMType!.Name))
            .ForMember(dest => dest.MotherboardFactor, opt => opt.MapFrom(src => src.Metadata.MotherboardFactor!.Name))
            .ForMember(dest => dest.Socket, opt => opt.MapFrom(src => src.Metadata.Socket!.Name))
            .ForMember(dest => dest.RAMMode, opt => opt.MapFrom(src => src.Metadata.RAMMode!.Name));

        CreateMap<PowerSupply, ClientPowerSupplyInfo>()
            .ForMember(dest => dest.MotherboardPowering, opt => opt.MapFrom(src => src.Metadata.MotherboardPowering!.Name));

        CreateMap<Processor, ClientProcessorInfo>()
            .ForMember(dest => dest.Socket, opt => opt.MapFrom(src => src.Metadata.Socket!.Name))
            .ForMember(dest => dest.IntegratedGraphics, opt => opt.MapFrom(src => src.Metadata.IntegratedGraphics!.Metadata.Title))
            .ForMember(dest => dest.TechProcess, opt => opt.MapFrom(src => src.Metadata.TechProcess!.Name));

        CreateMap<RandomAccessMemory, ClientRandomAccessMemoryInfo>()
            .ForMember(dest => dest.RAMType, opt => opt.MapFrom(src => src.Metadata.RAMType!.Name));

        CreateMap<SolidStateDrive, ClientSolidStateDriveInfo>()
            .ForMember(dest => dest.DiskConnectionInterface, opt => opt.MapFrom(src => src.Metadata.DiskConnectionInterface!.Name))
            .ForMember(dest => dest.DiskFactor, opt => opt.MapFrom(src => src.Metadata.DiskFactor!.Name));

        CreateMap<VideoCard, ClientVideoCardInfo>()
            .ForMember(dest => dest.VideoMemoryType, opt => opt.MapFrom(src => src.Metadata.VideoMemoryType!.Name));
    }
}
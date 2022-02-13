using AutoMapper;
using AutoMapper.EquivalencyExpression;
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
using FoundersPC.Application.Features.Producer.Models;
using FoundersPC.Domain.Entities.Hardware;
using FoundersPC.Domain.Entities.Hardware.Metadata;
using FoundersPC.Domain.Entities.Metadata;
using FoundersPC.SharedKernel.Models.Metadata;

namespace FoundersPC.Application.Mappings;

public class HardwareMappingProfile : Profile
{
    public HardwareMappingProfile()
    {
        CreateMap<MetadataEntity, MetadataInfo>()
            .IncludeAllDerived()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Name));

        CreateMap<MetadataInfo, MetadataEntity>()
            .EqualityComparison((src, dest) => src.Id == dest.Id)
            .IncludeAllDerived()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Value));

        CreateMap<Producer, MetadataInfo>()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.ShortName ?? src.FullName));

        CreateMap<VideoCard, MetadataInfo>()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Metadata.Title));

        #region GetHardwareRequest -> GetRequest

        CreateMap<GetHardwareRequest, Features.Hardware.Case.GetRequest>();
        CreateMap<GetHardwareRequest, Features.Hardware.HardDriveDisk.GetRequest>();
        CreateMap<GetHardwareRequest, Features.Hardware.Motherboard.GetRequest>();
        CreateMap<GetHardwareRequest, Features.Hardware.PowerSupply.GetRequest>();
        CreateMap<GetHardwareRequest, Features.Hardware.Processor.GetRequest>();
        CreateMap<GetHardwareRequest, Features.Hardware.RandomAccessMemory.GetRequest>();
        CreateMap<GetHardwareRequest, Features.Hardware.SolidStateDrive.GetRequest>();
        CreateMap<GetHardwareRequest, Features.Hardware.VideoCard.GetRequest>();

        #endregion

        #region HardwareInfo -> Hardware

        CreateMap<HardwareInfo, Hardware>()
            .IncludeAllDerived()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Created, opt => opt.Ignore())
            .ForMember(dest => dest.LastModified, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.Deleted, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedBy, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedById, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedById, opt => opt.Ignore())
            .ForMember(dest => dest.HardwareType, opt => opt.Ignore());
        CreateMap<HardwareInfo, HardwareMetadata>()
            .IncludeAllDerived()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.Ignore())
            .ForMember(dest => dest.Hardware, opt => opt.Ignore())
            .ForMember(dest => dest.HardwareType, opt => opt.Ignore());

        #endregion

        #region Hardware -> HardwareInfo

        CreateMap<Hardware, HardwareInfo>()
            .IncludeAllDerived()
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.Login))
            .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(src => src.LastModifiedBy.Login));
        CreateMap<HardwareMetadata, HardwareInfo>()
            .IncludeAllDerived();

        #endregion

        #region Producer

        CreateMap<Producer, ProducerInfo>()
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.Login))
            .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(src => src.LastModifiedBy.Login));
        CreateMap<ProducerInfo, Producer>()
            .IncludeAllDerived()
            .ForMember(dest => dest.LastModified, opt => opt.Ignore())
            .ForMember(dest => dest.Created, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Created, opt => opt.Ignore())
            .ForMember(dest => dest.LastModified, opt => opt.Ignore());
        CreateMap<Producer, ProducerViewInfo>();
        CreateMap<Features.Producer.CreateRequest, Producer>();
        CreateMap<Features.Producer.UpdateRequest, Producer>();

        #endregion

        #region Case

        CreateMap<Case, CaseInfo>()
            .IncludeMembers(x => x.Metadata);
        CreateMap<CaseMetadata, CaseInfo>();

        CreateMap<Case, CaseViewInfo>()
            .IncludeMembers(src => src.Metadata)
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.Login))
            .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(src => src.LastModifiedBy.Login));
        CreateMap<CaseMetadata, CaseViewInfo>();

        CreateMap<Features.Hardware.Case.CreateRequest, Case>();
        CreateMap<Features.Hardware.Case.CreateRequest, CaseMetadata>();

        CreateMap<Features.Hardware.Case.UpdateRequest, Case>()
            .ForMember(dest => dest.Metadata, opt => opt.MapFrom(src => src));
        CreateMap<Features.Hardware.Case.UpdateRequest, CaseMetadata>();

        #endregion

        #region HardDriveDisk

        CreateMap<HardDriveDisk, HardDriveDiskInfo>()
            .IncludeMembers(x => x.Metadata);
        CreateMap<HardDriveDiskMetadata, HardDriveDiskInfo>();

        CreateMap<HardDriveDisk, HardDriveDiskViewInfo>()
            .IncludeMembers(src => src.Metadata)
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.Login))
            .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(src => src.LastModifiedBy.Login));
        CreateMap<HardDriveDiskMetadata, HardDriveDiskViewInfo>();

        CreateMap<Features.Hardware.HardDriveDisk.CreateRequest, HardDriveDisk>();
        CreateMap<Features.Hardware.HardDriveDisk.CreateRequest, HardDriveDiskMetadata>();

        CreateMap<Features.Hardware.HardDriveDisk.UpdateRequest, HardDriveDisk>()
            .ForMember(dest => dest.Metadata, opt => opt.MapFrom(src => src));
        CreateMap<Features.Hardware.HardDriveDisk.UpdateRequest, HardDriveDiskMetadata>();

        #endregion

        #region Motherboard

        CreateMap<Motherboard, MotherboardInfo>()
            .IncludeMembers(x => x.Metadata);
        CreateMap<MotherboardMetadata, MotherboardInfo>();

        CreateMap<Motherboard, MotherboardViewInfo>()
            .IncludeMembers(src => src.Metadata)
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.Login))
            .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(src => src.LastModifiedBy.Login));
        CreateMap<MotherboardMetadata, MotherboardViewInfo>();

        CreateMap<Features.Hardware.Motherboard.CreateRequest, Motherboard>();
        CreateMap<Features.Hardware.Motherboard.CreateRequest, MotherboardMetadata>();

        CreateMap<Features.Hardware.Motherboard.UpdateRequest, Motherboard>()
            .ForMember(dest => dest.Metadata, opt => opt.MapFrom(src => src));
        CreateMap<Features.Hardware.Motherboard.UpdateRequest, MotherboardMetadata>();

        #endregion

        #region PowerSupply

        CreateMap<PowerSupply, PowerSupplyInfo>()
            .IncludeMembers(x => x.Metadata);
        CreateMap<PowerSupplyMetadata, PowerSupplyInfo>();

        CreateMap<PowerSupply, PowerSupplyViewInfo>()
            .IncludeMembers(src => src.Metadata)
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.Login))
            .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(src => src.LastModifiedBy.Login));
        CreateMap<PowerSupplyMetadata, PowerSupplyViewInfo>();

        CreateMap<Features.Hardware.PowerSupply.CreateRequest, PowerSupply>();
        CreateMap<Features.Hardware.PowerSupply.CreateRequest, PowerSupplyMetadata>();

        CreateMap<Features.Hardware.PowerSupply.UpdateRequest, PowerSupply>()
            .ForMember(dest => dest.Metadata, opt => opt.MapFrom(src => src));
        CreateMap<Features.Hardware.PowerSupply.UpdateRequest, PowerSupplyMetadata>();

        #endregion

        #region Processor

        CreateMap<Processor, ProcessorInfo>()
            .IncludeMembers(x => x.Metadata);
        CreateMap<ProcessorMetadata, ProcessorInfo>();

        CreateMap<Processor, ProcessorViewInfo>()
            .IncludeMembers(src => src.Metadata)
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.Login))
            .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(src => src.LastModifiedBy.Login));
        CreateMap<ProcessorMetadata, ProcessorViewInfo>();

        CreateMap<Features.Hardware.Processor.CreateRequest, Processor>();
        CreateMap<Features.Hardware.Processor.CreateRequest, ProcessorMetadata>();

        CreateMap<Features.Hardware.Processor.UpdateRequest, Processor>()
            .ForMember(dest => dest.Metadata, opt => opt.MapFrom(src => src));
        CreateMap<Features.Hardware.Processor.UpdateRequest, ProcessorMetadata>();

        #endregion

        #region RandomAccessMemory

        CreateMap<RandomAccessMemory, RandomAccessMemoryInfo>()
            .IncludeMembers(x => x.Metadata);
        CreateMap<RandomAccessMemoryMetadata, RandomAccessMemoryInfo>();

        CreateMap<RandomAccessMemory, RandomAccessMemoryViewInfo>()
            .IncludeMembers(src => src.Metadata)
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.Login))
            .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(src => src.LastModifiedBy.Login));
        CreateMap<RandomAccessMemoryMetadata, RandomAccessMemoryViewInfo>();

        CreateMap<Features.Hardware.RandomAccessMemory.CreateRequest, RandomAccessMemory>();
        CreateMap<Features.Hardware.RandomAccessMemory.CreateRequest, RandomAccessMemoryMetadata>();

        CreateMap<Features.Hardware.RandomAccessMemory.UpdateRequest, RandomAccessMemory>()
            .ForMember(dest => dest.Metadata, opt => opt.MapFrom(src => src));
        CreateMap<Features.Hardware.RandomAccessMemory.UpdateRequest, RandomAccessMemoryMetadata>();

        #endregion

        #region SolidStateDrive

        CreateMap<SolidStateDrive, SolidStateDriveInfo>()
            .IncludeMembers(x => x.Metadata);
        CreateMap<SolidStateDriveMetadata, SolidStateDriveInfo>();

        CreateMap<SolidStateDrive, SolidStateDriveViewInfo>()
            .IncludeMembers(src => src.Metadata)
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.Login))
            .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(src => src.LastModifiedBy.Login));
        CreateMap<SolidStateDriveMetadata, SolidStateDriveViewInfo>();

        CreateMap<Features.Hardware.SolidStateDrive.CreateRequest, SolidStateDrive>();
        CreateMap<Features.Hardware.SolidStateDrive.CreateRequest, SolidStateDriveMetadata>();

        CreateMap<Features.Hardware.SolidStateDrive.UpdateRequest, SolidStateDrive>()
            .ForMember(dest => dest.Metadata, opt => opt.MapFrom(src => src));
        CreateMap<Features.Hardware.SolidStateDrive.UpdateRequest, SolidStateDriveMetadata>();

        #endregion

        #region VideoCard

        CreateMap<VideoCard, VideoCardInfo>()
            .IncludeMembers(x => x.Metadata);
        CreateMap<VideoCardMetadata, VideoCardInfo>();

        CreateMap<VideoCard, VideoCardViewInfo>()
            .IncludeMembers(src => src.Metadata)
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.Login))
            .ForMember(dest => dest.LastModifiedBy, opt => opt.MapFrom(src => src.LastModifiedBy.Login));
        CreateMap<VideoCardMetadata, VideoCardViewInfo>();

        CreateMap<Features.Hardware.VideoCard.CreateRequest, VideoCard>();
        CreateMap<Features.Hardware.VideoCard.CreateRequest, VideoCardMetadata>();

        CreateMap<Features.Hardware.VideoCard.UpdateRequest, VideoCard>()
            .ForMember(dest => dest.Metadata, opt => opt.MapFrom(src => src));
        CreateMap<Features.Hardware.VideoCard.UpdateRequest, VideoCardMetadata>();

        #endregion
    }
}
#region Using namespaces

using AutoMapper;
using FoundersPC.API.Domain.Entities;
using FoundersPC.API.Domain.Entities.Memory;
using FoundersPC.API.Domain.Entities.Processor;
using FoundersPC.API.Domain.Entities.VideoCard;
using FoundersPC.API.Dto;

#endregion

namespace FoundersPC.API.Application.Mappings
{
    internal class MappingStartup : Profile
    {
        public MappingStartup()
        {
            #region ProducerEntity Mapping

            CreateMap<ProducerEntity, ProducerReadDto>()
                .ReverseMap();

            CreateMap<ProducerEntity, ProducerInsertDto>()
                .ReverseMap();

            CreateMap<ProducerEntity, ProducerUpdateDto>()
                .ReverseMap();

            #endregion

            #region CaseEntity Mapping

            CreateMap<CaseEntity, CaseReadDto>()
                .ReverseMap();

            CreateMap<CaseEntity, CaseInsertDto>()
                .ReverseMap();

            CreateMap<CaseEntity, CaseUpdateDto>()
                .ReverseMap();

            #endregion

            #region VideoCardEntity Mapping

            CreateMap<VideoCardEntity, VideoCardReadDto>()
                .ReverseMap();

            CreateMap<VideoCardEntity, VideoCardInsertDto>()
                .ReverseMap();

            CreateMap<VideoCardEntity, VideoCardUpdateDto>()
                .ReverseMap();

            #endregion

            #region HardDriveDiskEntity Mapping

            CreateMap<HardDriveDiskEntity, HardDriveDiskReadDto>()
                .ReverseMap();

            CreateMap<HardDriveDiskEntity, HardDriveDiskInsertDto>()
                .ReverseMap();

            CreateMap<HardDriveDiskEntity, HardDriveDiskUpdateDto>()
                .ReverseMap();

            #endregion

            #region MotherboardEntity Mapping

            CreateMap<MotherboardEntity, MotherboardReadDto>()
                .ReverseMap();

            CreateMap<MotherboardEntity, MotherboardInsertDto>()
                .ReverseMap();

            CreateMap<MotherboardEntity, MotherboardUpdateDto>()
                .ReverseMap();

            #endregion

            #region PowerSupplyEntity Mapping

            CreateMap<PowerSupplyEntity, PowerSupplyReadDto>()
                .ReverseMap();

            CreateMap<PowerSupplyEntity, PowerSupplyInsertDto>()
                .ReverseMap();

            CreateMap<PowerSupplyEntity, PowerSupplyUpdateDto>()
                .ReverseMap();

            #endregion

            #region ProcessorEntity Mapping

            CreateMap<ProcessorEntity, ProcessorReadDto>();
            CreateMap<ProcessorEntity, ProcessorInsertDto>();
            CreateMap<ProcessorEntity, ProcessorUpdateDto>();

            #endregion

            #region RandomAccessMemoryEntity Mapping

            CreateMap<RandomAccessMemoryEntity, RandomAccessMemoryReadDto>();
            CreateMap<RandomAccessMemoryEntity, RandomAccessMemoryInsertDto>();
            CreateMap<RandomAccessMemoryEntity, RandomAccessMemoryUpdateDto>();

            #endregion

            #region SolidStateDriveEntity Mapping

            CreateMap<SolidStateDriveEntity, SolidStateDriveReadDto>();
            CreateMap<SolidStateDriveEntity, SolidStateDriveInsertDto>();
            CreateMap<SolidStateDriveEntity, SolidStateDriveUpdateDto>();

            #endregion

            #region ProcessorCore Mapping

            CreateMap<ProcessorCore, ProcessorCoreReadDto>();
            CreateMap<ProcessorCore, ProcessorCoreInsertDto>();
            CreateMap<ProcessorCore, ProcessorCoreUpdateDto>();

            #endregion

            #region VideoCardCoreEntity Mapping

            CreateMap<VideoCardCoreEntity, VideoCardCoreReadDto>();
            CreateMap<VideoCardCoreEntity, VideoCardCoreInsertDto>();
            CreateMap<VideoCardCoreEntity, VideoCardCoreUpdateDto>();

            #endregion
        }
    }
}
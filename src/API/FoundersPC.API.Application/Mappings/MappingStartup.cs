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

            CreateMap<ProcessorEntity, ProcessorReadDto>()
                .ReverseMap();

            CreateMap<ProcessorEntity, ProcessorInsertDto>()
                .ReverseMap();

            CreateMap<ProcessorEntity, ProcessorUpdateDto>()
                .ReverseMap();

            #endregion

            #region RandomAccessMemoryEntity Mapping

            CreateMap<RandomAccessMemoryEntity, RandomAccessMemoryReadDto>()
                .ReverseMap();

            CreateMap<RandomAccessMemoryEntity, RandomAccessMemoryInsertDto>()
                .ReverseMap();

            CreateMap<RandomAccessMemoryEntity, RandomAccessMemoryUpdateDto>()
                .ReverseMap();

            #endregion

            #region SolidStateDriveEntity Mapping

            CreateMap<SolidStateDriveEntity, SolidStateDriveReadDto>()
                .ReverseMap();

            CreateMap<SolidStateDriveEntity, SolidStateDriveInsertDto>()
                .ReverseMap();

            CreateMap<SolidStateDriveEntity, SolidStateDriveUpdateDto>()
                .ReverseMap();

            #endregion

            #region ProcessorCore Mapping

            CreateMap<ProcessorCoreEntity, ProcessorCoreReadDto>()
                .ReverseMap();

            CreateMap<ProcessorCoreEntity, ProcessorCoreInsertDto>()
                .ReverseMap();

            CreateMap<ProcessorCoreEntity, ProcessorCoreUpdateDto>()
                .ReverseMap();

            #endregion

            #region VideoCardCoreEntity Mapping

            CreateMap<VideoCardCoreEntity, VideoCardCoreReadDto>()
                .ReverseMap();

            CreateMap<VideoCardCoreEntity, VideoCardCoreInsertDto>()
                .ReverseMap();

            CreateMap<VideoCardCoreEntity, VideoCardCoreUpdateDto>()
                .ReverseMap();

            #endregion
        }
    }
}
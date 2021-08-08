#region Using namespaces

using AutoMapper;
using FoundersPC.API.Domain.Entities;
using FoundersPC.API.Domain.Entities.Hardware;
using FoundersPC.API.Domain.Entities.Hardware.Memory;
using FoundersPC.API.Domain.Entities.Hardware.Processor;
using FoundersPC.API.Domain.Entities.Hardware.VideoCard;
using FoundersPC.API.Dto;

#endregion

namespace FoundersPC.API.Application.Mappings
{
    internal class MappingStartup : Profile
    {
        public MappingStartup()
        {
            #region ProducerEntity Mapping

            CreateMap<Producer, ProducerReadDto>()
                .ReverseMap();

            CreateMap<Producer, ProducerInsertDto>()
                .ReverseMap();

            CreateMap<Producer, ProducerUpdateDto>()
                .ReverseMap();

            #endregion

            #region CaseEntity Mapping

            CreateMap<Case, CaseReadDto>()
                .ReverseMap();

            CreateMap<Case, CaseInsertDto>()
                .ReverseMap();

            CreateMap<Case, CaseUpdateDto>()
                .ReverseMap();

            #endregion

            #region VideoCardEntity Mapping

            CreateMap<VideoCard, VideoCardReadDto>()
                .ReverseMap();

            CreateMap<VideoCard, VideoCardInsertDto>()
                .ReverseMap();

            CreateMap<VideoCard, VideoCardUpdateDto>()
                .ReverseMap();

            #endregion

            #region HardDriveDiskEntity Mapping

            CreateMap<HardDriveDisk, HardDriveDiskReadDto>()
                .ReverseMap();

            CreateMap<HardDriveDisk, HardDriveDiskInsertDto>()
                .ReverseMap();

            CreateMap<HardDriveDisk, HardDriveDiskUpdateDto>()
                .ReverseMap();

            #endregion

            #region MotherboardEntity Mapping

            CreateMap<Motherboard, MotherboardReadDto>()
                .ReverseMap();

            CreateMap<Motherboard, MotherboardInsertDto>()
                .ReverseMap();

            CreateMap<Motherboard, MotherboardUpdateDto>()
                .ReverseMap();

            #endregion

            #region PowerSupplyEntity Mapping

            CreateMap<PowerSupply, PowerSupplyReadDto>()
                .ReverseMap();

            CreateMap<PowerSupply, PowerSupplyInsertDto>()
                .ReverseMap();

            CreateMap<PowerSupply, PowerSupplyUpdateDto>()
                .ReverseMap();

            #endregion

            #region ProcessorEntity Mapping

            CreateMap<Processor, ProcessorReadDto>()
                .ReverseMap();

            CreateMap<Processor, ProcessorInsertDto>()
                .ReverseMap();

            CreateMap<Processor, ProcessorUpdateDto>()
                .ReverseMap();

            #endregion

            #region RandomAccessMemoryEntity Mapping

            CreateMap<RandomAccessMemory, RandomAccessMemoryReadDto>()
                .ReverseMap();

            CreateMap<RandomAccessMemory, RandomAccessMemoryInsertDto>()
                .ReverseMap();

            CreateMap<RandomAccessMemory, RandomAccessMemoryUpdateDto>()
                .ReverseMap();

            #endregion

            #region SolidStateDriveEntity Mapping

            CreateMap<SolidStateDrive, SolidStateDriveReadDto>()
                .ReverseMap();

            CreateMap<SolidStateDrive, SolidStateDriveInsertDto>()
                .ReverseMap();

            CreateMap<SolidStateDrive, SolidStateDriveUpdateDto>()
                .ReverseMap();

            #endregion

            #region ProcessorCore Mapping

            CreateMap<ProcessorCore, ProcessorCoreReadDto>()
                .ReverseMap();

            CreateMap<ProcessorCore, ProcessorCoreInsertDto>()
                .ReverseMap();

            CreateMap<ProcessorCore, ProcessorCoreUpdateDto>()
                .ReverseMap();

            #endregion

            #region VideoCardCoreEntity Mapping

            CreateMap<VideoCardCore, VideoCardCoreReadDto>()
                .ReverseMap();

            CreateMap<VideoCardCore, VideoCardCoreInsertDto>()
                .ReverseMap();

            CreateMap<VideoCardCore, VideoCardCoreUpdateDto>()
                .ReverseMap();

            #endregion
        }
    }
}
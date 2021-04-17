#region Using namespaces

using AutoMapper;

#endregion

namespace FoundersPC.API.Dto.Mapping
{
    public class HardwareApiDtoMapping : Profile
    {
        public HardwareApiDtoMapping()
        {
            #region Producer

            CreateMap<ProducerReadDto, ProducerUpdateDto>()
                .ReverseMap();

            CreateMap<ProducerReadDto, ProducerInsertDto>();

            #endregion

            #region Case

            CreateMap<CaseReadDto, CaseUpdateDto>()
                .ReverseMap();

            CreateMap<CaseReadDto, CaseInsertDto>();

            #endregion

            #region GPU

            CreateMap<VideoCardReadDto, VideoCardUpdateDto>()
                .ReverseMap();

            CreateMap<VideoCardReadDto, VideoCardInsertDto>();

            #endregion

            #region HDD

            CreateMap<HardDriveDiskReadDto, HardDriveDiskUpdateDto>()
                .ReverseMap();

            CreateMap<HardDriveDiskReadDto, HardDriveDiskInsertDto>();

            #endregion

            #region Motherboard

            CreateMap<MotherboardReadDto, MotherboardUpdateDto>()
                .ReverseMap();

            CreateMap<MotherboardReadDto, MotherboardInsertDto>();

            #endregion

            #region Power Supply

            CreateMap<PowerSupplyReadDto, PowerSupplyUpdateDto>()
                .ReverseMap();

            CreateMap<PowerSupplyReadDto, PowerSupplyInsertDto>();

            #endregion

            #region CPU

            CreateMap<ProcessorReadDto, ProcessorUpdateDto>()
                .ReverseMap();

            CreateMap<ProcessorReadDto, ProcessorInsertDto>();

            #endregion

            #region RAM

            CreateMap<RandomAccessMemoryUpdateDto, RandomAccessMemoryUpdateDto>()
                .ReverseMap();

            CreateMap<RandomAccessMemoryReadDto, RandomAccessMemoryInsertDto>();

            #endregion

            #region SSD

            CreateMap<SolidStateDriveReadDto, SolidStateDriveUpdateDto>()
                .ReverseMap();

            CreateMap<SolidStateDriveReadDto, SolidStateDriveInsertDto>();

            #endregion

            #region CPU Core

            CreateMap<ProcessorCoreReadDto, ProcessorCoreUpdateDto>()
                .ReverseMap();

            CreateMap<ProcessorCoreReadDto, ProcessorCoreInsertDto>();

            #endregion

            #region GPU Core

            CreateMap<VideoCardCoreReadDto, VideoCardCoreUpdateDto>()
                .ReverseMap();

            CreateMap<VideoCardCoreReadDto, VideoCardCoreInsertDto>();

            #endregion
        }
    }
}
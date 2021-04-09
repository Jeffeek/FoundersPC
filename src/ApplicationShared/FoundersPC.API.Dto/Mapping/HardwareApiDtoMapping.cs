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

            CreateMap<GPUReadDto, GPUUpdateDto>()
                .ReverseMap();

            CreateMap<GPUReadDto, GPUInsertDto>();

            #endregion

            #region HDD

            CreateMap<HDDReadDto, HDDUpdateDto>()
                .ReverseMap();

            CreateMap<HDDReadDto, HDDInsertDto>();

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

            CreateMap<CPUReadDto, CPUUpdateDto>()
                .ReverseMap();

            CreateMap<CPUReadDto, CPUInsertDto>();

            #endregion

            #region RAM

            CreateMap<RAMUpdateDto, RAMUpdateDto>()
                .ReverseMap();

            CreateMap<RAMReadDto, RAMInsertDto>();

            #endregion

            #region SSD

            CreateMap<SSDReadDto, SSDUpdateDto>()
                .ReverseMap();

            CreateMap<SSDReadDto, SSDInsertDto>();

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
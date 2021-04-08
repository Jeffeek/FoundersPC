#region Using namespaces

using AutoMapper;

#endregion

namespace FoundersPC.API.Dto.Mapping
{
    public class HardwareApiDtoMapping : Profile
    {
        public HardwareApiDtoMapping()
        {
            CreateMap<ProducerReadDto, ProducerUpdateDto>()
                .ReverseMap();

            CreateMap<ProducerReadDto, ProducerInsertDto>();

            CreateMap<CaseReadDto, CaseUpdateDto>()
                .ReverseMap();

            CreateMap<CaseReadDto, CaseInsertDto>();

            CreateMap<GPUReadDto, GPUUpdateDto>()
                .ReverseMap();

            CreateMap<GPUReadDto, GPUInsertDto>();

            CreateMap<HDDReadDto, HDDUpdateDto>()
                .ReverseMap();

            CreateMap<HDDReadDto, HDDInsertDto>();

            CreateMap<MotherboardReadDto, MotherboardUpdateDto>()
                .ReverseMap();

            CreateMap<MotherboardReadDto, MotherboardInsertDto>();

            CreateMap<PowerSupplyReadDto, PowerSupplyUpdateDto>()
                .ReverseMap();

            CreateMap<PowerSupplyReadDto, PowerSupplyInsertDto>();

            CreateMap<CPUReadDto, CPUUpdateDto>()
                .ReverseMap();

            CreateMap<CPUReadDto, CPUInsertDto>();

            CreateMap<RAMUpdateDto, RAMUpdateDto>()
                .ReverseMap();

            CreateMap<RAMReadDto, RAMInsertDto>();

            CreateMap<SSDReadDto, SSDUpdateDto>()
                .ReverseMap();

            CreateMap<SSDReadDto, SSDInsertDto>();

            CreateMap<ProcessorCoreReadDto, ProcessorCoreUpdateDto>()
                .ReverseMap();

            CreateMap<ProcessorCoreReadDto, ProcessorCoreInsertDto>();

            CreateMap<VideoCardCoreReadDto, VideoCardCoreUpdateDto>()
                .ReverseMap();

            CreateMap<VideoCardCoreReadDto, VideoCardCoreInsertDto>();
        }
    }
}
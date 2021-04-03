#region Using namespaces

using AutoMapper;
using FoundersPC.API.Domain.Entities.Hardware;
using FoundersPC.API.Domain.Entities.Hardware.Memory;
using FoundersPC.API.Domain.Entities.Hardware.Processor;
using FoundersPC.API.Domain.Entities.Hardware.VideoCard;
using FoundersPC.API.Dto;

#endregion

namespace FoundersPC.API.Application.Mappings
{
    public class MappingStartup : Profile
    {
        public MappingStartup()
        {
            #region Producer Mapping

            CreateMap<Producer, ProducerReadDto>().ReverseMap();
            CreateMap<Producer, ProducerInsertDto>().ReverseMap();
            CreateMap<Producer, ProducerUpdateDto>().ReverseMap();

            #endregion

            #region Case Mapping

            CreateMap<Case, CaseReadDto>().ReverseMap();
            CreateMap<Case, CaseInsertDto>().ReverseMap();
            CreateMap<Case, CaseUpdateDto>().ReverseMap();

            #endregion

            #region GPU Mapping

            CreateMap<GPU, GPUReadDto>().ReverseMap();
            CreateMap<GPU, GPUInsertDto>().ReverseMap();
            CreateMap<GPU, GPUUpdateDto>().ReverseMap();

            #endregion

            #region HDD Mapping

            CreateMap<HDD, HDDReadDto>().ReverseMap();
            CreateMap<HDD, HDDInsertDto>().ReverseMap();
            CreateMap<HDD, HDDUpdateDto>().ReverseMap();

            #endregion

            #region Motherboard Mapping

            CreateMap<Motherboard, MotherboardReadDto>().ReverseMap();
            CreateMap<Motherboard, MotherboardInsertDto>().ReverseMap();
            CreateMap<Motherboard, MotherboardUpdateDto>().ReverseMap();

            #endregion

            #region PowerSupply Mapping

            CreateMap<PowerSupply, PowerSupplyReadDto>().ReverseMap();
            CreateMap<PowerSupply, PowerSupplyInsertDto>().ReverseMap();
            CreateMap<PowerSupply, PowerSupplyUpdateDto>().ReverseMap();

            #endregion

            #region CPU Mapping

            CreateMap<CPU, CPUReadDto>();
            CreateMap<CPU, CPUInsertDto>();
            CreateMap<CPU, CPUUpdateDto>();

            #endregion

            #region RAM Mapping

            CreateMap<RAM, RAMReadDto>();
            CreateMap<RAM, RAMInsertDto>();
            CreateMap<RAM, RAMUpdateDto>();

            #endregion

            #region SSD Mapping

            CreateMap<SSD, SSDReadDto>();
            CreateMap<SSD, SSDInsertDto>();
            CreateMap<SSD, SSDUpdateDto>();

            #endregion

            #region ProcessorCore Mapping

            CreateMap<ProcessorCore, ProcessorCoreReadDto>();
            CreateMap<ProcessorCore, ProcessorCoreInsertDto>();
            CreateMap<ProcessorCore, ProcessorCoreUpdateDto>();

            #endregion

            #region VideoCardCore Mapping

            CreateMap<VideoCardCore, VideoCardCoreReadDto>();
            CreateMap<VideoCardCore, VideoCardCoreInsertDto>();
            CreateMap<VideoCardCore, VideoCardCoreUpdateDto>();

            #endregion
        }
    }
}
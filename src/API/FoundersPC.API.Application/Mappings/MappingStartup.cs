#region Using namespaces

using AutoMapper;
using FoundersPC.API.Domain.Entities.Hardware;
using FoundersPC.API.Domain.Entities.Hardware.Memory;
using FoundersPC.API.Domain.Entities.Hardware.Processor;
using FoundersPC.API.Domain.Entities.Hardware.VideoCard;

#endregion

namespace FoundersPC.API.Application.Mappings
{
    public class MappingStartup : Profile
    {
        public MappingStartup()
        {
            #region Producer Mapping

            CreateMap<Producer, ProducerReadDto>();
            CreateMap<Producer, ProducerInsertDto>();
            CreateMap<Producer, ProducerUpdateDto>();

            CreateMap<ProducerReadDto, Producer>();
            CreateMap<ProducerReadDto, ProducerUpdateDto>();
            CreateMap<ProducerReadDto, ProducerInsertDto>();

            CreateMap<ProducerUpdateDto, Producer>();
            CreateMap<ProducerUpdateDto, ProducerReadDto>();
            CreateMap<ProducerUpdateDto, ProducerInsertDto>();

            CreateMap<ProducerInsertDto, Producer>();
            CreateMap<ProducerInsertDto, ProducerReadDto>();
            CreateMap<ProducerInsertDto, ProducerUpdateDto>();

            #endregion

            #region Case Mapping

            CreateMap<Case, CaseReadDto>();
            CreateMap<Case, CaseInsertDto>();
            CreateMap<Case, CaseUpdateDto>();

            CreateMap<CaseReadDto, Case>();
            CreateMap<CaseReadDto, CaseUpdateDto>();
            CreateMap<CaseReadDto, CaseInsertDto>();

            CreateMap<CaseUpdateDto, Case>();
            CreateMap<CaseUpdateDto, CaseReadDto>();
            CreateMap<CaseUpdateDto, CaseInsertDto>();

            CreateMap<CaseInsertDto, Case>();
            CreateMap<CaseInsertDto, CaseReadDto>();
            CreateMap<CaseInsertDto, CaseUpdateDto>();

            #endregion

            #region GPU Mapping

            CreateMap<GPU, GPUReadDto>();
            CreateMap<GPU, GPUInsertDto>();
            CreateMap<GPU, GPUUpdateDto>();

            CreateMap<GPUReadDto, GPU>();
            CreateMap<GPUReadDto, GPUUpdateDto>();
            CreateMap<GPUReadDto, GPUInsertDto>();

            CreateMap<GPUUpdateDto, GPU>();
            CreateMap<GPUUpdateDto, GPUReadDto>();
            CreateMap<GPUUpdateDto, GPUInsertDto>();

            CreateMap<GPUInsertDto, GPU>();
            CreateMap<GPUInsertDto, GPUReadDto>();
            CreateMap<GPUInsertDto, GPUUpdateDto>();

            #endregion

            #region HDD Mapping

            CreateMap<HDD, HDDReadDto>();
            CreateMap<HDD, HDDInsertDto>();
            CreateMap<HDD, HDDUpdateDto>();

            CreateMap<HDDReadDto, HDD>();
            CreateMap<HDDReadDto, HDDUpdateDto>();
            CreateMap<HDDReadDto, HDDInsertDto>();

            CreateMap<HDDUpdateDto, HDD>();
            CreateMap<HDDUpdateDto, HDDReadDto>();
            CreateMap<HDDUpdateDto, HDDInsertDto>();

            CreateMap<HDDInsertDto, HDD>();
            CreateMap<HDDInsertDto, HDDReadDto>();
            CreateMap<HDDInsertDto, HDDUpdateDto>();

            #endregion

            #region Motherboard Mapping

            CreateMap<Motherboard, MotherboardReadDto>();
            CreateMap<Motherboard, MotherboardInsertDto>();
            CreateMap<Motherboard, MotherboardUpdateDto>();

            CreateMap<MotherboardReadDto, Motherboard>();
            CreateMap<MotherboardReadDto, MotherboardUpdateDto>();
            CreateMap<MotherboardReadDto, MotherboardInsertDto>();

            CreateMap<MotherboardUpdateDto, Motherboard>();
            CreateMap<MotherboardUpdateDto, MotherboardReadDto>();
            CreateMap<MotherboardUpdateDto, MotherboardInsertDto>();

            CreateMap<MotherboardInsertDto, Motherboard>();
            CreateMap<MotherboardInsertDto, MotherboardReadDto>();
            CreateMap<MotherboardInsertDto, MotherboardUpdateDto>();

            #endregion

            #region PowerSupply Mapping

            CreateMap<PowerSupply, PowerSupplyReadDto>();
            CreateMap<PowerSupply, PowerSupplyInsertDto>();
            CreateMap<PowerSupply, PowerSupplyUpdateDto>();

            CreateMap<PowerSupplyReadDto, PowerSupply>();
            CreateMap<PowerSupplyReadDto, PowerSupplyUpdateDto>();
            CreateMap<PowerSupplyReadDto, PowerSupplyInsertDto>();

            CreateMap<PowerSupplyUpdateDto, PowerSupply>();
            CreateMap<PowerSupplyUpdateDto, PowerSupplyReadDto>();
            CreateMap<PowerSupplyUpdateDto, PowerSupplyInsertDto>();

            CreateMap<PowerSupplyInsertDto, PowerSupply>();
            CreateMap<PowerSupplyInsertDto, PowerSupplyReadDto>();
            CreateMap<PowerSupplyInsertDto, PowerSupplyUpdateDto>();

            #endregion

            #region CPU Mapping

            CreateMap<CPU, CPUReadDto>();
            CreateMap<CPU, CPUInsertDto>();
            CreateMap<CPU, CPUUpdateDto>();

            CreateMap<CPUReadDto, CPU>();
            CreateMap<CPUReadDto, CPUUpdateDto>();
            CreateMap<CPUReadDto, CPUInsertDto>();

            CreateMap<CPUUpdateDto, CPU>();
            CreateMap<CPUUpdateDto, CPUReadDto>();
            CreateMap<CPUUpdateDto, CPUInsertDto>();

            CreateMap<CPUInsertDto, CPU>();
            CreateMap<CPUInsertDto, CPUReadDto>();
            CreateMap<CPUInsertDto, CPUUpdateDto>();

            #endregion

            #region RAM Mapping

            CreateMap<RAM, RAMReadDto>();
            CreateMap<RAM, RAMInsertDto>();
            CreateMap<RAM, RAMUpdateDto>();

            CreateMap<RAMReadDto, RAM>();
            CreateMap<RAMReadDto, RAMUpdateDto>();
            CreateMap<RAMReadDto, RAMInsertDto>();

            CreateMap<RAMUpdateDto, RAM>();
            CreateMap<RAMUpdateDto, RAMReadDto>();
            CreateMap<RAMUpdateDto, RAMInsertDto>();

            CreateMap<RAMInsertDto, RAM>();
            CreateMap<RAMInsertDto, RAMReadDto>();
            CreateMap<RAMInsertDto, RAMUpdateDto>();

            #endregion

            #region SSD Mapping

            CreateMap<SSD, SSDReadDto>();
            CreateMap<SSD, SSDInsertDto>();
            CreateMap<SSD, SSDUpdateDto>();

            CreateMap<SSDReadDto, SSD>();
            CreateMap<SSDReadDto, SSDUpdateDto>();
            CreateMap<SSDReadDto, SSDInsertDto>();

            CreateMap<SSDUpdateDto, SSD>();
            CreateMap<SSDUpdateDto, SSDReadDto>();
            CreateMap<SSDUpdateDto, SSDInsertDto>();

            CreateMap<SSDInsertDto, SSD>();
            CreateMap<SSDInsertDto, SSDReadDto>();
            CreateMap<SSDInsertDto, SSDUpdateDto>();

            #endregion

            #region ProcessorCore Mapping

            CreateMap<ProcessorCore, ProcessorCoreReadDto>();
            CreateMap<ProcessorCore, ProcessorCoreInsertDto>();
            CreateMap<ProcessorCore, ProcessorCoreUpdateDto>();

            CreateMap<ProcessorCoreReadDto, ProcessorCore>();
            CreateMap<ProcessorCoreReadDto, ProcessorCoreUpdateDto>();
            CreateMap<ProcessorCoreReadDto, ProcessorCoreInsertDto>();

            CreateMap<ProcessorCoreUpdateDto, ProcessorCore>();
            CreateMap<ProcessorCoreUpdateDto, ProcessorCoreReadDto>();
            CreateMap<ProcessorCoreUpdateDto, ProcessorCoreInsertDto>();

            CreateMap<ProcessorCoreInsertDto, ProcessorCore>();
            CreateMap<ProcessorCoreInsertDto, ProcessorCoreReadDto>();
            CreateMap<ProcessorCoreInsertDto, ProcessorCoreUpdateDto>();

            #endregion

            #region VideoCardCore Mapping

            CreateMap<VideoCardCore, VideoCardCoreReadDto>();
            CreateMap<VideoCardCore, VideoCardCoreInsertDto>();
            CreateMap<VideoCardCore, VideoCardCoreUpdateDto>();

            CreateMap<VideoCardCoreReadDto, VideoCardCore>();
            CreateMap<VideoCardCoreReadDto, VideoCardCoreUpdateDto>();
            CreateMap<VideoCardCoreReadDto, VideoCardCoreInsertDto>();

            CreateMap<VideoCardCoreUpdateDto, VideoCardCore>();
            CreateMap<VideoCardCoreUpdateDto, VideoCardCoreReadDto>();
            CreateMap<VideoCardCoreUpdateDto, VideoCardCoreInsertDto>();

            CreateMap<VideoCardCoreInsertDto, VideoCardCore>();
            CreateMap<VideoCardCoreInsertDto, VideoCardCoreReadDto>();
            CreateMap<VideoCardCoreInsertDto, VideoCardCoreUpdateDto>();

            #endregion
        }
    }
}
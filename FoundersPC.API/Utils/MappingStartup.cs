﻿#region Using derectives

using AutoMapper;
using FoundersPC.Services.DTO;
using FoundersPC.Services.Models.Hardware;
using FoundersPC.Services.Models.Hardware.Memory;

#endregion

namespace FoundersPC.API.Utils
{
	public class MappingStartup : Profile
	{
		public MappingStartup()
		{
			CreateMap<ProducerReadDto, Producer>();
			CreateMap<Producer, ProducerReadDto>();

			CreateMap<CPU, CPUReadDto>();
			CreateMap<CPU, CPUUpdateDto>();
			CreateMap<CPU, CPUInsertDto>();
			CreateMap<CPUReadDto, CPU>();
			CreateMap<CPUReadDto, CPUUpdateDto>();
			CreateMap<CPUInsertDto, CPU>();
			CreateMap<CPUUpdateDto, CPU>();

			CreateMap<ProcessorLineupReadDto, ProcessorLineup>();
			CreateMap<ProcessorLineup, ProcessorLineupReadDto>();

			CreateMap<CaseReadDto, Case>();
			CreateMap<Case, CaseReadDto>();

			CreateMap<HDDReadDto, HDD>();
			CreateMap<HDD, HDDReadDto>();

			CreateMap<SSDReadDto, SSD>();
			CreateMap<SSD, SSDReadDto>();

			CreateMap<MotherboardReadDto, Motherboard>();
			CreateMap<Motherboard, MotherboardReadDto>();

			CreateMap<PowerSupplyReadDto, PowerSupply>();
			CreateMap<PowerSupply, PowerSupplyReadDto>();

			CreateMap<RAMReadDto, RAM>();
			CreateMap<RAM, RAMReadDto>();

			CreateMap<GPUReadDto, GPU>();
			CreateMap<GPU, GPUReadDto>();
		}
	}
}
#region Using derectives

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

			CreateMap<CPUReadDto, CPU>();
			CreateMap<CPU, CPUReadDto>();

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
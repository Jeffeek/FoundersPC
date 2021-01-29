#region Using derectives

using AutoMapper;
using FoundersPC.Services.DTO;
using FoundersPC.Services.Models.Hardware;

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
		}
	}
}
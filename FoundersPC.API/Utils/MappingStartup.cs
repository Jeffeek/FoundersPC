#region Using derectives

using AutoMapper;
using FoundersPC.Services.DTO;
using FoundersPC.Services.Models;

#endregion

namespace FoundersPC.API.Utils
{
	public class MappingStartup : Profile
	{
		public MappingStartup()
		{
			CreateMap<ProducerDto, Producer>();
			CreateMap<Producer, ProducerDto>();
		}
	}
}
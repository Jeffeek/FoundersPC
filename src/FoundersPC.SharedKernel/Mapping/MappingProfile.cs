using AutoMapper;
using AutoMapper.EquivalencyExpression;
using FoundersPC.Domain.Entities.Hardware;
using FoundersPC.Domain.Entities.Metadata;
using FoundersPC.SharedKernel.Models.Metadata;

namespace FoundersPC.SharedKernel.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MetadataEntity, MetadataInfo>()
            .IncludeAllDerived()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Name));

        CreateMap<MetadataInfo, MetadataEntity>()
            .EqualityComparison((src, dest) => src.Id == dest.Id)
            .IncludeAllDerived()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Value));

        CreateMap<Producer, MetadataInfo>()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.ShortName ?? src.FullName));

        CreateMap<VideoCard, MetadataInfo>()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Metadata.Title));
    }
}
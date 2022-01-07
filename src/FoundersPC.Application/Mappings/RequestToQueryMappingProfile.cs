using AutoMapper;
using FoundersPC.Domain.Enums;

namespace FoundersPC.Application.Mappings;

public class RequestToQueryMappingProfile : Profile
{
    public RequestToQueryMappingProfile()
    {
        CreateMap<Features.Hardware.Case.GetAllRequest, Features.Hardware.Case.GetAllQuery>();
        //CreateMap<Features.Hardware.Case.GetAllRequest, Features.Hardware.Case.GetAllQuery>();
        //CreateMap<Features.Hardware.Case.GetAllRequest, Features.Hardware.Case.GetAllQuery>();
        //CreateMap<Features.Hardware.Case.GetAllRequest, Features.Hardware.Case.GetAllQuery>();
        //CreateMap<Features.Hardware.Case.GetAllRequest, Features.Hardware.Case.GetAllQuery>();
        //CreateMap<Features.Hardware.Case.GetAllRequest, Features.Hardware.Case.GetAllQuery>();
        //CreateMap<Features.Hardware.Case.GetAllRequest, Features.Hardware.Case.GetAllQuery>();
        //CreateMap<Features.Hardware.Case.GetAllRequest, Features.Hardware.Case.GetAllQuery>();

        CreateMap<Features.Hardware.Case.GetRequest, Features.Hardware.Case.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.Case));

        CreateMap<Features.Hardware.Case.UpdateRequest, Features.Hardware.Case.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.Case));

        CreateMap<Features.Hardware.Case.DeleteRequest, Features.Hardware.Case.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.Case));

        CreateMap<Features.Hardware.HardDriveDisk.GetRequest, Features.Hardware.HardDriveDisk.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.HDD));

        CreateMap<Features.Hardware.HardDriveDisk.UpdateRequest, Features.Hardware.HardDriveDisk.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.HDD));

        CreateMap<Features.Hardware.HardDriveDisk.DeleteRequest, Features.Hardware.HardDriveDisk.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.HDD));

        CreateMap<Features.Hardware.Motherboard.GetRequest, Features.Hardware.Motherboard.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.MB));

        CreateMap<Features.Hardware.Motherboard.UpdateRequest, Features.Hardware.Motherboard.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.MB));

        CreateMap<Features.Hardware.Motherboard.DeleteRequest, Features.Hardware.Motherboard.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.MB));

        CreateMap<Features.Hardware.PowerSupply.GetRequest, Features.Hardware.PowerSupply.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.FPU));

        CreateMap<Features.Hardware.PowerSupply.UpdateRequest, Features.Hardware.PowerSupply.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.FPU));

        CreateMap<Features.Hardware.PowerSupply.DeleteRequest, Features.Hardware.PowerSupply.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.FPU));

        CreateMap<Features.Hardware.Processor.GetRequest, Features.Hardware.Processor.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.CPU));

        CreateMap<Features.Hardware.Processor.UpdateRequest, Features.Hardware.Processor.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.CPU));

        CreateMap<Features.Hardware.Processor.DeleteRequest, Features.Hardware.Processor.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.CPU));

        CreateMap<Features.Hardware.RandomAccessMemory.GetRequest, Features.Hardware.RandomAccessMemory.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.RAM));

        CreateMap<Features.Hardware.RandomAccessMemory.UpdateRequest, Features.Hardware.RandomAccessMemory.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.RAM));

        CreateMap<Features.Hardware.RandomAccessMemory.DeleteRequest, Features.Hardware.RandomAccessMemory.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.RAM));

        CreateMap<Features.Hardware.SolidStateDrive.GetRequest, Features.Hardware.SolidStateDrive.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.SSD));

        CreateMap<Features.Hardware.SolidStateDrive.UpdateRequest, Features.Hardware.SolidStateDrive.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.SSD));

        CreateMap<Features.Hardware.SolidStateDrive.DeleteRequest, Features.Hardware.SolidStateDrive.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.SSD));

        CreateMap<Features.Hardware.VideoCard.GetRequest, Features.Hardware.VideoCard.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.GPU));

        CreateMap<Features.Hardware.VideoCard.UpdateRequest, Features.Hardware.VideoCard.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.GPU));

        CreateMap<Features.Hardware.VideoCard.DeleteRequest, Features.Hardware.VideoCard.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.GPU));
    }
}
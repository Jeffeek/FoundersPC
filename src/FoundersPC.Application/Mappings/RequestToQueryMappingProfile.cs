﻿using AutoMapper;
using FoundersPC.Domain.Enums;

namespace FoundersPC.Application.Mappings;

public class RequestToQueryMappingProfile : Profile
{
    public RequestToQueryMappingProfile()
    {
        // Hardware
        CreateMap<Features.Hardware.Case.GetAllRequest, Features.Hardware.Case.GetAllQuery>();
        CreateMap<Features.Hardware.HardDriveDisk.GetAllRequest, Features.Hardware.HardDriveDisk.GetAllQuery>();
        CreateMap<Features.Hardware.Motherboard.GetAllRequest, Features.Hardware.Motherboard.GetAllQuery>();
        CreateMap<Features.Hardware.PowerSupply.GetAllRequest, Features.Hardware.PowerSupply.GetAllQuery>();
        CreateMap<Features.Hardware.Processor.GetAllRequest, Features.Hardware.Processor.GetAllQuery>();
        CreateMap<Features.Hardware.RandomAccessMemory.GetAllRequest, Features.Hardware.RandomAccessMemory.GetAllQuery>();
        CreateMap<Features.Hardware.SolidStateDrive.GetAllRequest, Features.Hardware.SolidStateDrive.GetAllQuery>();
        CreateMap<Features.Hardware.VideoCard.GetAllRequest, Features.Hardware.VideoCard.GetAllQuery>();
        CreateMap<Features.Producer.GetAllRequest, Features.Producer.GetAllQuery>();

        // Client
        CreateMap<Features.Client.Hardware.Case.GetAllRequest, Features.Client.Hardware.Case.GetAllQuery>();
        CreateMap<Features.Client.Hardware.HardDriveDisk.GetAllRequest, Features.Client.Hardware.HardDriveDisk.GetAllQuery>();
        CreateMap<Features.Client.Hardware.Motherboard.GetAllRequest, Features.Client.Hardware.Motherboard.GetAllQuery>();
        CreateMap<Features.Client.Hardware.PowerSupply.GetAllRequest, Features.Client.Hardware.PowerSupply.GetAllQuery>();
        CreateMap<Features.Client.Hardware.Processor.GetAllRequest, Features.Client.Hardware.Processor.GetAllQuery>();
        CreateMap<Features.Client.Hardware.RandomAccessMemory.GetAllRequest, Features.Client.Hardware.RandomAccessMemory.GetAllQuery>();
        CreateMap<Features.Client.Hardware.SolidStateDrive.GetAllRequest, Features.Client.Hardware.SolidStateDrive.GetAllQuery>();
        CreateMap<Features.Client.Hardware.VideoCard.GetAllRequest, Features.Client.Hardware.VideoCard.GetAllQuery>();
        CreateMap<Features.Client.Producer.GetAllRequest, Features.Client.Producer.GetAllQuery>();

        //User Info
        CreateMap<Features.UserInformation.GetAllRequest, Features.UserInformation.GetAllQuery>();

        CreateMap<Features.Hardware.Case.GetRequest, Features.Hardware.Case.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.Case));
        CreateMap<Features.Client.Hardware.Case.GetRequest, Features.Client.Hardware.Case.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.Case));
        CreateMap<Features.Hardware.Case.UpdateRequest, Features.Hardware.Case.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.Case));
        CreateMap<Features.Hardware.Case.DeleteRequest, Features.Hardware.Case.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.Case));
        CreateMap<Features.Hardware.Case.RestoreRequest, Features.Hardware.Case.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.Case));

        CreateMap<Features.Hardware.HardDriveDisk.GetRequest, Features.Hardware.HardDriveDisk.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.HDD));
        CreateMap<Features.Client.Hardware.HardDriveDisk.GetRequest, Features.Client.Hardware.HardDriveDisk.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.HDD));
        CreateMap<Features.Hardware.HardDriveDisk.UpdateRequest, Features.Hardware.HardDriveDisk.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.HDD));
        CreateMap<Features.Hardware.HardDriveDisk.DeleteRequest, Features.Hardware.HardDriveDisk.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.HDD));
        CreateMap<Features.Hardware.HardDriveDisk.RestoreRequest, Features.Hardware.HardDriveDisk.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.HDD));

        CreateMap<Features.Hardware.Motherboard.GetRequest, Features.Hardware.Motherboard.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.MB));
        CreateMap<Features.Client.Hardware.Motherboard.GetRequest, Features.Client.Hardware.Motherboard.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.MB));
        CreateMap<Features.Hardware.Motherboard.UpdateRequest, Features.Hardware.Motherboard.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.MB));
        CreateMap<Features.Hardware.Motherboard.DeleteRequest, Features.Hardware.Motherboard.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.MB));
        CreateMap<Features.Hardware.Motherboard.RestoreRequest, Features.Hardware.Motherboard.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.MB));

        CreateMap<Features.Hardware.PowerSupply.GetRequest, Features.Hardware.PowerSupply.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.PowerSupply));
        CreateMap<Features.Client.Hardware.PowerSupply.GetRequest, Features.Client.Hardware.PowerSupply.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.PowerSupply));
        CreateMap<Features.Hardware.PowerSupply.UpdateRequest, Features.Hardware.PowerSupply.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.PowerSupply));
        CreateMap<Features.Hardware.PowerSupply.DeleteRequest, Features.Hardware.PowerSupply.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.PowerSupply));
        CreateMap<Features.Hardware.PowerSupply.RestoreRequest, Features.Hardware.PowerSupply.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.PowerSupply));

        CreateMap<Features.Hardware.Processor.GetRequest, Features.Hardware.Processor.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.CPU));
        CreateMap<Features.Client.Hardware.Processor.GetRequest, Features.Client.Hardware.Processor.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.CPU));
        CreateMap<Features.Hardware.Processor.UpdateRequest, Features.Hardware.Processor.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.CPU));
        CreateMap<Features.Hardware.Processor.DeleteRequest, Features.Hardware.Processor.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.CPU));
        CreateMap<Features.Hardware.Processor.RestoreRequest, Features.Hardware.Processor.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.CPU));

        CreateMap<Features.Hardware.RandomAccessMemory.GetRequest, Features.Hardware.RandomAccessMemory.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.RAM));
        CreateMap<Features.Client.Hardware.RandomAccessMemory.GetRequest, Features.Client.Hardware.RandomAccessMemory.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.RAM));
        CreateMap<Features.Hardware.RandomAccessMemory.UpdateRequest, Features.Hardware.RandomAccessMemory.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.RAM));
        CreateMap<Features.Hardware.RandomAccessMemory.DeleteRequest, Features.Hardware.RandomAccessMemory.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.RAM));
        CreateMap<Features.Hardware.RandomAccessMemory.RestoreRequest, Features.Hardware.RandomAccessMemory.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.RAM));

        CreateMap<Features.Hardware.SolidStateDrive.GetRequest, Features.Hardware.SolidStateDrive.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.SSD));
        CreateMap<Features.Client.Hardware.SolidStateDrive.GetRequest, Features.Client.Hardware.SolidStateDrive.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.SSD));
        CreateMap<Features.Hardware.SolidStateDrive.UpdateRequest, Features.Hardware.SolidStateDrive.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.SSD));
        CreateMap<Features.Hardware.SolidStateDrive.DeleteRequest, Features.Hardware.SolidStateDrive.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.SSD));
        CreateMap<Features.Hardware.SolidStateDrive.RestoreRequest, Features.Hardware.SolidStateDrive.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.SSD));

        CreateMap<Features.Hardware.VideoCard.GetRequest, Features.Hardware.VideoCard.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.GPU));
        CreateMap<Features.Client.Hardware.VideoCard.GetRequest, Features.Client.Hardware.VideoCard.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.GPU));
        CreateMap<Features.Hardware.VideoCard.UpdateRequest, Features.Hardware.VideoCard.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.GPU));
        CreateMap<Features.Hardware.VideoCard.DeleteRequest, Features.Hardware.VideoCard.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.GPU));
        CreateMap<Features.Hardware.VideoCard.RestoreRequest, Features.Hardware.VideoCard.GetQuery>()
            .ForMember(dest => dest.HardwareTypeId, opt => opt.MapFrom(_ => (int)HardwareType.GPU));

        CreateMap<Features.Producer.GetRequest, Features.Producer.GetQuery>();
        CreateMap<Features.Client.Producer.GetRequest, Features.Client.Producer.GetQuery>();
        CreateMap<Features.Producer.UpdateRequest, Features.Producer.GetQuery>();
        CreateMap<Features.Producer.DeleteRequest, Features.Producer.GetQuery>();

        //User Info
        CreateMap<Features.UserInformation.GetAllRequest, Features.UserInformation.GetAllQuery>();
    }
}
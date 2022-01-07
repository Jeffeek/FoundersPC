using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.Processor.Models;
using FoundersPC.SharedKernel.Pagination;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.Processor;

public class GetAllRequest : GetAllHardwareRequest, IRequest<IPagedList<ProcessorViewInfo>> { }
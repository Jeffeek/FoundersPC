using FoundersPC.Application.Features.Client.Hardware.Base;
using FoundersPC.Application.Features.Client.Hardware.Processor.Models;
using FoundersPC.SharedKernel.Pagination;
using MediatR;

namespace FoundersPC.Application.Features.Client.Hardware.Processor;

public class GetAllRequest : GetAllHardwareRequest, IRequest<IPagedList<ClientProcessorInfo>> { }
using FoundersPC.Application.Features.Client.Hardware.Base;
using FoundersPC.Application.Features.Client.Hardware.RandomAccessMemory.Models;
using FoundersPC.SharedKernel.Pagination;
using MediatR;

namespace FoundersPC.Application.Features.Client.Hardware.RandomAccessMemory;

public class GetAllRequest : GetAllHardwareRequest, IRequest<IPagedList<ClientRandomAccessMemoryInfo>> { }
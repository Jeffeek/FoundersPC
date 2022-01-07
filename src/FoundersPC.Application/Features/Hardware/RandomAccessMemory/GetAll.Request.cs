using FoundersPC.Application.Features.Hardware.Base;
using FoundersPC.Application.Features.Hardware.RandomAccessMemory.Models;
using FoundersPC.SharedKernel.Pagination;
using MediatR;

namespace FoundersPC.Application.Features.Hardware.RandomAccessMemory;

public class GetAllRequest : GetAllHardwareRequest, IRequest<IPagedList<RandomAccessMemoryViewInfo>> { }
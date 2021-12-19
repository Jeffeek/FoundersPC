using FoundersPC.Application.Features.Metadata.Models;
using MediatR;

namespace FoundersPC.Application.Features.Metadata;

public class GetMetadataPackageRequest : IRequest<MetadataPackage> { }
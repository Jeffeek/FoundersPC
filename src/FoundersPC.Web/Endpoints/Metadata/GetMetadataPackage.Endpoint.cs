using System.Threading;
using System.Threading.Tasks;
using FoundersPC.Application.Features.Metadata;
using FoundersPC.Application.Features.Metadata.Models;
using FoundersPC.SharedKernel.Endpoints;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoundersPC.Web.Endpoints.Metadata;

public class GetMetadataPackageEndpoint : BaseResponseAnonymousEndpoint<MetadataPackage, GetMetadataPackageEndpoint>
{
    [HttpGet("Metadata")]
    [OpenApiOperation(operationId : "Metadata.Get",
                      summary : "Get Metadata package",
                      description : "Get Metadata package")]
    [OpenApiTags("Metadata")]
    public override async Task<MetadataPackage> HandleAsync(CancellationToken cancellationToken = new CancellationToken()) =>
        await Mediator.Send(new GetMetadataPackageRequest(), cancellationToken);
}
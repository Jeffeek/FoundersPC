using System.Threading;
using FoundersPC.Application.Features.Metadata;
using FoundersPC.Application.Features.Metadata.Models;
using FoundersPC.SharedKernel.Helpers;
using MediatR;

namespace FoundersPC.UI.Admin.Locators;

public class MetadataPackageLocator
{
    public MetadataPackage MetadataPackage { get; private set; }

    public int Id { get; set; }

    public MetadataPackageLocator(IMediator mediator)
    {
        Initialize(mediator);
        Id = Thread.CurrentThread.ManagedThreadId;
    }

    private void Initialize(IMediator mediator)
    {
        TaskHelper.RunSync(async () =>
                           {
                               MetadataPackage = await mediator.Send(new GetMetadataPackageRequest());
                               AddNullValues();
                           });
    }

    private void AddNullValues()
    {
        foreach (var list in MetadataPackage)
            list.Insert(0, new() { Id = -1, Value = "NO VALUE" });
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoMapper;
using FoundersPC.Application.Features.Metadata;
using FoundersPC.SharedKernel.Helpers;
using FoundersPC.SharedKernel.Models.Metadata;
using MediatR;

namespace FoundersPC.UI.Admin.Locators;

public class MetadataPackageViewModel : IEnumerable<ObservableCollection<MetadataInfo>>
{
    public ObservableCollection<MetadataInfo> CaseType { get; set; } = default!;
    public ObservableCollection<MetadataInfo> Color { get; set; } = default!;
    public ObservableCollection<MetadataInfo> Country { get; set; } = default!;
    public ObservableCollection<MetadataInfo> DiskConnectionInterface { get; set; } = default!;
    public ObservableCollection<MetadataInfo> DiskFactor { get; set; } = default!;
    public ObservableCollection<MetadataInfo> Material { get; set; } = default!;
    public ObservableCollection<MetadataInfo> MotherboardFactor { get; set; } = default!;
    public ObservableCollection<MetadataInfo> MotherboardPowering { get; set; } = default!;
    public ObservableCollection<MetadataInfo> RamMode { get; set; } = default!;
    public ObservableCollection<MetadataInfo> RamType { get; set; } = default!;
    public ObservableCollection<MetadataInfo> Socket { get; set; } = default!;
    public ObservableCollection<MetadataInfo> TechProcess { get; set; } = default!;
    public ObservableCollection<MetadataInfo> VideoMemory { get; set; } = default!;
    public ObservableCollection<MetadataInfo> WindowMaterial { get; set; } = default!;
    public ObservableCollection<MetadataInfo> Producers { get; set; } = default!;
    public ObservableCollection<MetadataInfo> IntegratedVideoCards { get; set; } = default!;

    public IEnumerator<ObservableCollection<MetadataInfo>> GetEnumerator()
    {
        yield return CaseType;
        yield return Color;
        yield return Country;
        yield return DiskConnectionInterface;
        yield return DiskFactor;
        yield return Material;
        yield return MotherboardFactor;
        yield return MotherboardPowering;
        yield return RamMode;
        yield return RamType;
        yield return Socket;
        yield return TechProcess;
        yield return VideoMemory;
        yield return WindowMaterial;
        yield return IntegratedVideoCards;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class MetadataPackageLocator
{
    private readonly IMapper _mapper;

    public MetadataPackageViewModel MetadataPackage { get; private set; } = default!;

    public MetadataPackageLocator(IMediator mediator, IMapper mapper)
    {
        _mapper = mapper;
        Initialize(mediator);
    }

    private void Initialize(IMediator mediator)
    {
        TaskHelper.RunSync(async () =>
                           {
                               MetadataPackage = _mapper.Map<MetadataPackageViewModel>(await mediator.Send(new GetMetadataPackageRequest()));
                               AddNullValues();
                           });
    }

    private void AddNullValues()
    {
        foreach (var list in MetadataPackage)
            list.Insert(0, new() { Id = -1, Value = "NO VALUE" });
    }
}
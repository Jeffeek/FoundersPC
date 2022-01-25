using System;
using FoundersPC.UI.Admin.Locators;
using Prism.Mvvm;

namespace FoundersPC.UI.Admin.Models;

public class ProducerInfoViewModel : BindableBase
{
    private int _id;
    public int Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    private string? _shortName;
    public string? ShortName
    {
        get => _shortName;
        set => SetProperty(ref _shortName, value);
    }

    private string _fullName = default!;
    public string FullName
    {
        get => _fullName;
        set
        {
            SetProperty(ref _fullName, value);
            RefreshLocator.FireSaveRefresh();
        }
    }

    private string? _website;
    public string? Website
    {
        get => _website;
        set => SetProperty(ref _website, value);
    }

    private DateTime? _foundationDate;
    public DateTime? FoundationDate
    {
        get => _foundationDate;
        set => SetProperty(ref _foundationDate, value);
    }

    private int? _countryId;
    public int? CountryId
    {
        get => _countryId ?? -1;
        set => SetProperty(ref _countryId, value);
    }
}
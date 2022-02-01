using System;
using Prism.Mvvm;

namespace FoundersPC.UI.Admin.Models;

public class AccessTokenViewModel : BindableBase
{
    private int _id;
    public int Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    private string _token = default!;
    public string Token
    {
        get => _token;
        set => SetProperty(ref _token, value);
    }

    private bool _isBlocked;
    public bool IsBlocked
    {
        get => _isBlocked;
        set => SetProperty(ref _isBlocked, value);
    }

    private DateTime _activeFrom;
    public DateTime ActiveFrom
    {
        get => _activeFrom;
        set => SetProperty(ref _activeFrom, value);
    }

    private DateTime _activeTo;
    public DateTime ActiveTo
    {
        get => _activeTo;
        set => SetProperty(ref _activeTo, value);
    }
}
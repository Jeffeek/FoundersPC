using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace FoundersPC.UI.Admin.Models;

public class UserInfoViewModel : BindableBase
{
    private int _id;
    private string _login = default!;
    private bool _isBlocked;
    private string _role = default!;

    public int Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    public string Login
    {
        get => _login;
        set => SetProperty(ref _login, value);
    }

    public bool IsBlocked
    {
        get => _isBlocked;
        set => SetProperty(ref _isBlocked, value);
    }

    public int RoleId { get; set; }

    public string Role
    {
        get => _role;
        set => SetProperty(ref _role, value);
    }

    private ObservableCollection<AccessTokenViewModel> _tokens = new();
    public ObservableCollection<AccessTokenViewModel> Tokens
    {
        get => _tokens;
        set => SetProperty(ref _tokens, value);
    }
}
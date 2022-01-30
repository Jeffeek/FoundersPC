using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace FoundersPC.UI.Admin.Models;

public class UserInfoViewModel : BindableBase
{
    private int _id;
    public int Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    private string _login = default!;
    public string Login
    {
        get => _login;
        set => SetProperty(ref _login, value);
    }

    private string _email = default!;
    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    private bool _isBlocked;
    public bool IsBlocked
    {
        get => _isBlocked;
        set => SetProperty(ref _isBlocked, value);
    }

    public int RoleId { get; set; }

    private string _role = default!;
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
using Prism.Mvvm;

namespace FoundersPC.UI.Admin.Models;

public class UserViewModel : BindableBase
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

    public string Role
    {
        get => _role;
        set => SetProperty(ref _role, value);
    }
}
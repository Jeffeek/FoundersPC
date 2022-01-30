using Prism.Mvvm;

namespace FoundersPC.UI.Admin.Models;

public class AccessTokenViewModel : BindableBase
{
    public int Id { get; set; }
    public string Token { get; set; }
    public bool IsBlocked { get; set; }
}
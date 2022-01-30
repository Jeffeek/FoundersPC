using System;
using Prism.Mvvm;

namespace FoundersPC.UI.Admin.Models;

public class AccessTokenViewModel : BindableBase
{
    public int Id { get; set; }
    public string Token { get; set; } = default!;
    public bool IsBlocked { get; set; }
    public DateTime ActiveFrom { get; set; }
    public DateTime ActiveTo { get; set; }
}
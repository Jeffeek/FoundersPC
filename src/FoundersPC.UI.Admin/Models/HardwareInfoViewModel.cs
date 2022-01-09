using FoundersPC.UI.Admin.Locators;
using Prism.Mvvm;

namespace FoundersPC.UI.Admin.Models;

public abstract class HardwareInfoViewModel : BindableBase
{
    public int Id { get; set; }
    public int HardwareTypeId { get; set; }

    private int _producerId;
    public int ProducerId
    {
        get => _producerId;
        set
        {
            SetProperty(ref _producerId, value);
            RefreshLocator.FireSaveRefresh();
        }
    }

    private string _title = default!;
    public string Title
    {
        get => _title;
        set
        {
            SetProperty(ref _title, value);
            RefreshLocator.FireSaveRefresh();
        }
    }
}
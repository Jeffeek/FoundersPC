using Prism.Mvvm;

namespace FoundersPC.UI.Admin.Locators;

public class FilterOptions : BindableBase
{
    private Pagination _pagination = new();

    public Pagination Pagination
    {
        get => _pagination;
        set => SetProperty(ref _pagination, value);
    }

    public OrderSettings OrderSettings { get; set; } = new();
    public string? SearchText { get; set; }
    public bool? ShowDeleted { get; set; }
}
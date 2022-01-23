using Prism.Mvvm;

namespace FoundersPC.UI.Admin.Locators;

public class Pagination : BindableBase
{
    private int _currentPage;

    public int CurrentPage
    {
        get => _currentPage;
        set
        {
            if (!SetProperty(ref _currentPage, value))
                return;

            RaisePropertyChanged(nameof(IsMoveBackAvailable));
            RaisePropertyChanged(nameof(IsMoveStraightAvailable));
        }
    }

    private int _currentPageSize;

    public int CurrentPageSize
    {
        get => _currentPageSize;
        set
        {
            if (!SetProperty(ref _currentPageSize, value))
                return;

            RaisePropertyChanged(nameof(IsMoveBackAvailable));
            RaisePropertyChanged(nameof(IsMoveStraightAvailable));
        }
    }

    private bool _isMoveBackAvailable;

    public bool IsMoveBackAvailable
    {
        get => _isMoveBackAvailable;
        set => SetProperty(ref _isMoveBackAvailable, value);
    }

    private bool _isMoveStraightAvailable;

    public bool IsMoveStraightAvailable
    {
        get => _isMoveStraightAvailable;
        set
        {
            if (!SetProperty(ref _isMoveStraightAvailable, value))
                return;

            RaisePropertyChanged(nameof(IsMoveBackAvailable));
            RaisePropertyChanged(nameof(IsMoveStraightAvailable));
        }
    }

    public int PageCount { get; set; }
}
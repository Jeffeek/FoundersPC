using System;
using FoundersPC.Application.Features.Hardware.Case.Models;

namespace FoundersPC.UI.Admin.Locators;

public class SelectedObjectLocator
{
    private CaseInfo? _selectedCase;

    public CaseInfo? SelectedCase
    {
        get => _selectedCase;
        set
        {
            if (value == _selectedCase)
                return;

            _selectedCase = value;
            SelectedCaseChanged?.Invoke(value);
        }
    }

    public event Action<CaseInfo?>? SelectedCaseChanged;
}
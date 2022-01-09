using System;
using System.Globalization;
using System.Windows.Controls;

namespace FoundersPC.UI.Admin.Validation;

public class NotEmptyValidationRule : ValidationRule
{
    public override ValidationResult Validate(object? value, CultureInfo cultureInfo) =>
        String.IsNullOrWhiteSpace((value ?? "").ToString())
            ? new(false, "Field is required.")
            : ValidationResult.ValidResult;
}
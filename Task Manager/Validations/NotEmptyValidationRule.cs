using System.Globalization;
using System.Windows.Controls;

namespace Task_Manager.Validations;

public class NotEmptyValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
        {
            return new ValidationResult(false, "All fields must be filled");
        }
        return ValidationResult.ValidResult;
    }
    
}
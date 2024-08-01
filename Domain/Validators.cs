namespace Api.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class TextOnlyCustomAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && !Regex.IsMatch(value?.ToString() ?? string.Empty, @"^[a-zA-Z\s]+$"))
            {
                return new ValidationResult($"El campo '{validationContext.DisplayName}' solo puede contener letras y espacios.");
            }
            return ValidationResult.Success;
        }
    }

    public class NumbersOnlyCustomAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && !Regex.IsMatch(value?.ToString() ?? string.Empty, @"^\d+$"))
            {
                return new ValidationResult($"El campo '{validationContext.DisplayName}' sólo puede contener números.");
            }
            return ValidationResult.Success;
        }
    }

    public class MaxLengthCustomAttribute : MaxLengthAttribute
    {
        public MaxLengthCustomAttribute(int length) : base(length)
        {
            ErrorMessage = "El campo '{0}' no puede tener más de '{1}' caracteres.";
        }
    }

    public class RequiredCustomAttribute : RequiredAttribute
    {
        public RequiredCustomAttribute() : base()
        {
            ErrorMessage = "El campo '{0}' es obligatorio.";
        }
    }
}

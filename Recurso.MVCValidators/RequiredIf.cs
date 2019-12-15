using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recurso.MVCValidators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class RequiredIfAttribute : RequiredAttribute
    {
        private readonly string _requiredProperty;

        public RequiredIfAttribute(string requiredProperty)
        {
            _requiredProperty = requiredProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(_requiredProperty);

            if (property == null)
            {
                throw new ArgumentException("Property with this name not found");
            }

            var comparisonValue = (bool)property.GetValue(validationContext.ObjectInstance);

            if (comparisonValue == false)
            {
                return ValidationResult.Success;
            }

            var compareDateDisplayName = property.GetCustomAttributes(typeof(DisplayAttribute), true).Cast<DisplayAttribute>().FirstOrDefault().Name;

            var errorMessage = $"{validationContext.DisplayName} is required when {compareDateDisplayName} field is checked";

            if (string.IsNullOrWhiteSpace(value?.ToString()))
            {
                return new ValidationResult(errorMessage);
            }

            if (Int32.TryParse(value?.ToString(), out int selectedValue) && selectedValue == -1)
            {
                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
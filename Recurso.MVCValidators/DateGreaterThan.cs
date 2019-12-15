using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recurso.MVCValidators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _dateToCompareProperty;

        public DateGreaterThanAttribute(string dateToCompareProperty)
        {
            _dateToCompareProperty = dateToCompareProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(_dateToCompareProperty);

            if (property == null)
            {
                throw new ArgumentException("Property with this name not found");
            }

            if (value == null || !DateTime.TryParse(value.ToString(), out DateTime currentValue) || !DateTime.TryParse(property.GetValue(validationContext.ObjectInstance).ToString(), out DateTime comparisonValue))
            {
                return ValidationResult.Success;
            }

            if (currentValue.Date > comparisonValue.Date)
            {
                return ValidationResult.Success;
            }
            else
            {
                var compareDateDisplayName = property.GetCustomAttributes(typeof(DisplayAttribute), true).Cast<DisplayAttribute>().FirstOrDefault().Name;
                return new ValidationResult(validationContext.DisplayName + " cannot be less than " + compareDateDisplayName);
            }
        }
    }
}
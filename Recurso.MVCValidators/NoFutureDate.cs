using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recurso.MVCValidators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class NoFutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !DateTime.TryParse(value.ToString(), out DateTime currentValue))
            {
                return ValidationResult.Success;
            }

            if (currentValue.Date > DateTime.Now.Date)
            {
                return new ValidationResult($"{validationContext.DisplayName} cannot be in the future");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
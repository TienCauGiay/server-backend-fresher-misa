using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.DTO.Employees.CustomValidate
{
    /// <summary>
    /// Custom validate identity number
    /// </summary>
    /// Created By: BNTIEN (07/07/2023)
    public class ValidateIdentityNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null && value.ToString()?.Trim() != string.Empty)
            {
                var regexAttribute = new RegularExpressionAttribute($"{Resources.ResourceVN.Regex_Number}");
                if (!regexAttribute.IsValid(value))
                {
                    return new ValidationResult(Resources.ResourceVN.Validate_IdentityNumber_Error);
                }
            }
            return ValidationResult.Success;
        }
    }
}

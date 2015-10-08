using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ModelValidators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    sealed public class StringRangeValidator : ValidationAttribute
    {
        public List<string> RangeStrings { get; private set; }
        public StringRangeValidator(string range)
        {
            if (string.IsNullOrWhiteSpace(range) || string.IsNullOrEmpty(range))
            {
                throw new ArgumentNullException("Parameter invalid: range");
            }
            RangeStrings = range.Trim().Split(new char[] { ',' }).ToList();

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var text = (string)value;
            if (string.IsNullOrEmpty(text) || RangeStrings.Contains(text))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(this.ErrorMessage);
            }
        }
    }
}
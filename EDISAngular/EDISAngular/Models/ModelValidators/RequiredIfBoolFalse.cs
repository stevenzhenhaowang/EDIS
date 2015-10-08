using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ModelValidators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class RequiredIfBoolFalse : ValidationAttribute
    {

        private string _propertyName;
        public RequiredIfBoolFalse(string propertyName)
        {
            _propertyName = propertyName;
        }


        protected override ValidationResult IsValid
            (object value, ValidationContext validationContext)
        {
            string valueString = "";
            try
            {
                valueString = (string)value;
            }
            catch (Exception)
            {
                return new ValidationResult("Property is invalid");
            }

            var correspondingPropertyValue = validationContext.ObjectType.GetProperty(_propertyName).GetValue(validationContext.ObjectInstance, null);
            try
            {
                var correspondingPropertyValueBool = (bool)correspondingPropertyValue;
                if (!correspondingPropertyValueBool && !string.IsNullOrEmpty(valueString))
                {
                    return ValidationResult.Success;
                }
                if (correspondingPropertyValueBool)
                {
                    return ValidationResult.Success;
                }
            }
            catch (Exception)
            {

                return new ValidationResult("Cannot retrieve corresponding property");
            }

            return new ValidationResult(ErrorMessage);
        }




    }
}
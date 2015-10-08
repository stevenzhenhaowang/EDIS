using EDIS_DOMAIN;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EDISAngular.Infrastructure.DatabaseAccess;

namespace EDISAngular.Models.ModelValidators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ProductTypeMustExisting : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }


            int valueNumber = -1;
            try
            {
                valueNumber = Int32.Parse(value.ToString());
            }
            catch (Exception)
            {

                return new ValidationResult("Property is invalid");
            }
            if (valueNumber < 0)
            {

                return new ValidationResult("Property is invalid");
            }

            using (CommonReferenceDataRepository db = new CommonReferenceDataRepository())
            {
                var productType = db.GetAllProductTypes().SingleOrDefault(s => s.id == valueNumber);
                if (productType != null)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
        }
    }
}
using EDIS_DOMAIN;
using EDISAngular.Infrastructure.DbFirst;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ModelValidators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ResourceTokenMustExisting : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
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
            if (string.IsNullOrEmpty(valueString))
            {
                return ValidationResult.Success;
            }

            //using (edisDbEntities db = new edisDbEntities())
            //{
            //    var adviser = db.ResourcesReferences.SingleOrDefault(s => s.tokenValue == valueString);
            //    if (adviser != null)
            //    {
            //        return ValidationResult.Success;
            //    }
            //    else
            //    {
            //        return new ValidationResult(ErrorMessage);
            //    }
            //}

            return null;
        }
    }
}
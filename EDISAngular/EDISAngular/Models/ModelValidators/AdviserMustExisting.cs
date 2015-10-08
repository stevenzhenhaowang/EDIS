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
    public class AdviserAccountNumberMustExisting : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string valueString = "";
            try
            {
                valueString = value.ToString();
            }
            catch (Exception)
            {
                return new ValidationResult("Property is invalid");
            }
            if (string.IsNullOrEmpty(valueString))
            {
                return ValidationResult.Success;
            }
            //int valueNumber = -1;
            //try
            //{
            //    valueNumber = Convert.ToInt32(valueString);
            //}
            //catch (Exception)
            //{

            //    //return new ValidationResult("Property is invalid");
            //}
            //if (valueNumber < 0)
            //{

            //    return new ValidationResult("Property is invalid");
            //}


            using (edisDbEntities db = new edisDbEntities())
            {
                var adviser = db.Advisers.SingleOrDefault(s => (s.AdviserNumber == valueString) || s.AdviserId == valueString); //AccountNumber --> AdviserNumber
                if (adviser != null)
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
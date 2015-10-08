using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EDISAngular;
using EDIS_DOMAIN;
using EDISAngular.Infrastructure.DbFirst;

namespace EDISAngular.Models.ModelValidators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ClientGroupMustExisting : ValidationAttribute
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

            using (edisDbEntities db = new edisDbEntities())
            {
                var clientGroup = db.ClientGroups.SingleOrDefault(s => s.ClientGroupId == valueString);
                if (clientGroup != null)
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
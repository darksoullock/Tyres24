using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelmesBootcamp.Models.Validators
{
    public class NoPastDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime && ((DateTime)value).Subtract(DateTime.Now).TotalMinutes < 0)
            {
                return new ValidationResult("Date cannot be in the past");
            }

            return null;
        }
    }
}
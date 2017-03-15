using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelmesBootcamp.Models.Validators
{
    public class MaxDurationAttribute : ValidationAttribute
    {
        int hours;

        public MaxDurationAttribute(int hours)
        {
            this.hours = hours;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var startProperty = validationContext.ObjectType.GetProperty(nameof(DbBooking.StartDateTime));
            var endProperty = validationContext.ObjectType.GetProperty(nameof(DbBooking.EndDateTime));
            DateTime start = (DateTime)startProperty.GetValue(validationContext.ObjectInstance);
            DateTime end = (DateTime)endProperty.GetValue(validationContext.ObjectInstance);
            double diffHours = (end - start).TotalHours;

            if (diffHours>hours || diffHours<=0)
            {
                return new ValidationResult("time between start date and end date should be 2 hours at most");
            }
            return null;
        }
    }
}
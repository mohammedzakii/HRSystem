
using System;
using System.ComponentModel.DataAnnotations;

namespace HRSystem.CustomValidation
{
    public class DateCompareAttribute : ValidationAttribute
    {

        // "{0} Date should be less than current"
        //public DateCompareAttribute() : base("{0} Applicant should be over than 20 years old")
        //{


        //}

        public override bool IsValid(object value)
        {
            DateTime pro = Convert.ToDateTime(value); //Convert.ToDateTime(value);
            
            if (pro.Year <= 2002 )
            {
                return true;
            }
            else
            {
                return false;
            }
            //return base.IsValid(value);
        }
    }
}
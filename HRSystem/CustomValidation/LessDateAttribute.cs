
using System;
using System.ComponentModel.DataAnnotations;

namespace HRSystem.CustomValidation
{
    public class LessDateAttribute : ValidationAttribute
    {

       // "{0} Date should be less than current"
        public LessDateAttribute() : base("{0} Date should be less than current")
        {


        }

        public override bool IsValid(object value)
        {
            DateTime pro = Convert.ToDateTime(value);
            if (pro <= DateTime.Now)
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
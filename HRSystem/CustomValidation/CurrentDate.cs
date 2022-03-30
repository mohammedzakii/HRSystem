using System;
using System.ComponentModel.DataAnnotations;
namespace HRsystem.CustomValidation
{
    public class CurrentDate:ValidationAttribute
    {
        public CurrentDate():base("{0} Should be greater than current Date")
        {

        }
        public override bool IsValid(object value)
        {
            DateTime propValue=Convert.ToDateTime(value);
            if(propValue > DateTime.Now)
            {
                return true;
            }
            else
            {
                
                return false;   
            }
        }
    }
}

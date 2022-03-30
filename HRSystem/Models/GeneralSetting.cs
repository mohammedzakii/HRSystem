using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRSystem.Models
{
    public enum Days { Saturday=1, Sunday, Monday, Tuesday, Wednesday, Thursday, Friday };
    public class GeneralSetting
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Extra required")]
        public int ExtraHours { get; set; }
        [Required(ErrorMessage ="Discount required")]
        public int DiscountHours { get; set; }
        [Required(ErrorMessage = "please Enter this Field")]
        public Days OffDAy1 { get; set; }
        [Required(ErrorMessage = "please Enter this Field")]
        public Days OffDAy2 { get; set; }




    }
}

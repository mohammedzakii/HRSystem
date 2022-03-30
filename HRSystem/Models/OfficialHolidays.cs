using HRsystem.CustomValidation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRSystem.Models
{
    public class OfficialHolidays
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Column(TypeName = "date")]
        [CurrentDate]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

    }
}

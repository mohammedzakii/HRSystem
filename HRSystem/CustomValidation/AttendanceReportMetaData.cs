using HRSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRSystem.CustomValidation
{
    public class AttendanceReportMetaData
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Leaving Time Required")]
        [DataType(DataType.Time)]
        [Remote(action: "testTime", controller: "AttentanceReport", AdditionalFields = "AttendanceTime", ErrorMessage = "Please Enter a Valid Time")]
        public DateTime LeavingTime { get; set; }

        [Required(ErrorMessage = "Attendance Time Required")]
        [DataType(DataType.Time)]
        public DateTime AttendanceTime { get; set; }

        [Required(ErrorMessage = "Date Required")]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        [Remote(action: "testDate", controller: "AttentanceReport", ErrorMessage = "Please Enter a Valid Date")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Id Required")]
        [ForeignKey("Employee")]
        [Remote(action: "TestId", controller: "AttentanceReport", ErrorMessage = "Please Enter a Valid Id")]
        public int EmpId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}

using HRsystem.CustomValidation;
using HRSystem.CustomValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRSystem.Models
{
    [ModelMetadataType(typeof(AttendanceReportMetaData))]
    public class AttendanceReport
    {
        public int Id { get; set; }
        public DateTime LeavingTime { get; set; }
        public DateTime AttendanceTime { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("Employee")]
        public int EmpId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}

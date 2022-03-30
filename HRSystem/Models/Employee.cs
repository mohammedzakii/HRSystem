using HRSystem.CustomValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRSystem.Models
{
    [ModelMetadataType(typeof(EmployeeMetaData))]


    public class Employee
    {

         
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        
        public string Email { get; set; }
        
        public string Phone { get; set; }
        public string Gender { get; set; }
 
        public DateTime DateOfBirth { get; set; }
        public long NationalID { get; set; }
        public string Nationality { get; set; }
        public decimal Salary { get; set; }
        
        public DateTime DateOfContract { get; set; }
        public DateTime AttendanceTime { get; set; }
        public DateTime LeavingTime { get; set; }
        public string Notes { get; set; }
        [ForeignKey("Department")]
        public int DeptID { get; set; }
        public virtual Department Department { get; set; }

        public List<AttendanceReport> attendanceReports { get; set; }

    }
}



using HRSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRSystem.CustomValidation
{
    public class EmployeeMetaData
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Address Required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Email Required")] 
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "the email must be like example@s.com")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone Required")]
        [MaxLength(11, ErrorMessage = "Enter a Vaild Number"), MinLength(11, ErrorMessage = "Enter a Vaild Number")]
        [Remote(action: "testPhone", controller: "Employee", ErrorMessage = "Enter a valid Number")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Gender Required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Date Of Birth Required")]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        //[Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:D}", ApplyFormatInEditMode = false)]
        [Remote(action: "testBirth", controller: "Employee", ErrorMessage = "Applicant should be over than 20 years old")]
        //[DateCompare(ErrorMessage ="data not valid")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "NationalID Required")]
        //[MaxLength(14, ErrorMessage = "Enter a Vaild Number"), MinLength(14, ErrorMessage = "Enter a Vaild Number")]
        [Remote(action: "testNationalID", controller: "Employee", ErrorMessage = "NationalID Shoud be valid Number")]
        public long NationalID { get; set; }
        [Required(ErrorMessage = "Nationality Required")]
        public string Nationality { get; set; }
        [Required(ErrorMessage = "Salary Required")]
        [Remote(action: "testSalary", controller: "Employee", ErrorMessage = "Enter a valid Number")]
        public decimal Salary { get; set; }
        [Required(ErrorMessage = "DateOfContract Required")]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        //[Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:D}", ApplyFormatInEditMode = false)]
        //[LessDate]
        [Remote(action: "testDate", controller: "Employee", ErrorMessage = "Enter a valid date")]
        public DateTime DateOfContract { get; set; }
        [Required(ErrorMessage = "AttendanceTime Required")]
        public DateTime AttendanceTime { get; set; }
        [Required(ErrorMessage = "LeavingTime Required")]
        [Remote(action: "testTime", controller: "Employee", AdditionalFields = "AttendanceTime", ErrorMessage = "Please Enter a Valid Time")]
        public DateTime LeavingTime { get; set; }
        public string Notes { get; set; }
        [ForeignKey("Department")]
        [Required(ErrorMessage = "Department Required")]
        public int DeptID { get; set; }
        public virtual Department Department { get; set; }


    }
}
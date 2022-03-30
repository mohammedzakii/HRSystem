using HRSystem.Models;
using HRSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HRSystem.Controllers
{
    public class SalaryReportController : Controller
    {
        IEmployeeRepository employeeRepository;

        IOfficialHolidaysRepository officialHolidaysRepository;
        IGeneralSettingRepository generalSettingServices;
        IAttentanceReportRepository attentanceReportRepository;

        public SalaryReportController(IEmployeeRepository employee, IOfficialHolidaysRepository official, IGeneralSettingRepository generalSetting, IAttentanceReportRepository attentanceReport)
        {
            employeeRepository = employee;
            generalSettingServices = generalSetting;
            officialHolidaysRepository = official;
            attentanceReportRepository = attentanceReport;

        }
        public IActionResult Index(string EmpName = null,int mnth=0,int year=0)
        {
            List<Employee> employees = employeeRepository.GetAll();
            List<AttendanceReport> reports = attentanceReportRepository.GetAll();
            GeneralSetting setting = generalSettingServices.Get();
            List<OfficialHolidays> officialHolidays = officialHolidaysRepository.GetAll();
            if (EmpName != null)
            {
                reports = reports.Where(e => e.Employee.Name.Contains(EmpName)).ToList();
                employees = employees.Where(e => e.Name.Contains(EmpName)).ToList();
            }
            int[] arr = {0,31,28,31,30,31,30,31,31,30,31,30,31 };
            int Days = arr[mnth]-8;

            foreach(var day in officialHolidays)
            {
                if(day.Date.Month == mnth && day.Date.Year == year)
                {
                    Days--;
                }
            }
            
            ViewData["Days"] = Days;
            ViewData["EmpName"] = EmpName;
            ViewData["year"] = year;
            ViewData["mnth"] = mnth;
            ViewData["setting"] = setting;
            ViewData["Holidays"] = officialHolidays;
            return View(employees);
        }

        
        public IActionResult Details(string EmpName="",string DeptName="",decimal Salary = 0, int AttendanceDays = 0, int AbsenceDays = 0, int bonusHours = 0, int DiscountHours = 0, decimal Totalbonus = 0, decimal TotalDiscount=0,decimal Total=0)
        {
            ViewData["EmpName"] = EmpName;
            ViewData["DeptName"] = DeptName;
            ViewData["Salary"] = Salary;
            ViewData["AttendanceDays"] = AttendanceDays;
            ViewData["AbsenceDays"] = AbsenceDays;
            ViewData["bonusHours"] = bonusHours;
            ViewData["DiscountHours"] = DiscountHours;
            ViewData["Totalbonus"] = Totalbonus;
            ViewData["TotalDiscount"] = TotalDiscount;
            ViewData["Total"] = Total;
            return View();
        }
    }
}

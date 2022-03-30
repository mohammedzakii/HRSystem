using HRSystem.Models;
using HRSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HRSystem.Controllers
{
    public class EmployeeController : Controller
    {
        IEmployeeRepository employeeRepository;
        IDepartmentRepository departmentRepository;
        public EmployeeController(IEmployeeRepository employee, IDepartmentRepository department)
        {
            employeeRepository = employee;
            departmentRepository = department;
        }

        // GET: EmoplyeeController
        public IActionResult GetEmployees(int ID=0,string EmpName = null, int DeptID = 0)
        {
            List<Employee> employees = employeeRepository.GetAll(); 
            List<Department> departments = departmentRepository.GetAll();
            if (EmpName != null)
            {
                employees = employees.Where(e => e.Name.Contains(EmpName)).ToList();
            }
            if (DeptID != 0)
            {
                employees = employees.Where(e => e.DeptID == DeptID).ToList();
            }
            ViewData["EmpName"] = EmpName;
            ViewData["DeptId"] = DeptID;
            ViewData["Depts"] = departments;
            ViewData["id"]=ID;
            return View(employees);
        }

        // GET: EmoplyeeController/Create
        public IActionResult Create()
        {
            List<Department> departments = departmentRepository.GetAll();
            ViewData["Depts"] = departments;
            return View();
        }

        // POST: EmoplyeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmployee(Employee employee)
        {
            if (ModelState.IsValid && employee.DeptID != 0)
            {
                employeeRepository.Insert(employee);
                return RedirectToAction("GetEmployees");
            }

            return RedirectToAction("create");


        }

        public IActionResult DeleteEmp(int id)
        {
            if (id != 0)
            {
                employeeRepository.Delete(id);
            }
            return RedirectToAction("GetEmployees");
        }

        public IActionResult Edit(int id)
        {
            Employee Emp = employeeRepository.GetById(id);
            List<Department> depts = departmentRepository.GetAll();
            ViewData["depts"] = depts;

            return View(Emp);
        }
        public IActionResult SaveEdit(int id, Employee newEmp)
        {
            List<Department> depts = departmentRepository.GetAll();
            ViewData["depts"] = depts;

            if (ModelState.IsValid == true)
            {
                employeeRepository.Update(id, newEmp);
                return RedirectToAction("GetEmployees");
            }
            return View("Edit", newEmp);
        }




        //-------------------------Employee Validation------------------------////

        public bool testDate(DateTime DateOfContract)
        {

            if (DateOfContract.Year >= 2008)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool testBirth(DateTime DateOfBirth)
        {

            if (DateOfBirth.Year >= 2002)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool testTime(DateTime LeavingTime, DateTime AttendanceTime)
        {

            if (LeavingTime <= AttendanceTime)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public bool testSalary(string Salary)
        {
            decimal sal; ;
            if (decimal.TryParse(Salary, out sal))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public IActionResult testNationalID(string NationalID)
        {


            if (long.TryParse(NationalID, out long n) && NationalID.Length == 14)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }


        public bool testPhone(string Phone)
        {
            int Pho;
            if (int.TryParse(Phone, out Pho))
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

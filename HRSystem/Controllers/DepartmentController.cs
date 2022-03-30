using HRSystem.Models;
using HRSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace HRSystem.Controllers
{
    public class DepartmentController : Controller
    {  /* HRSystemContext context=new HRSystemContext(); */
        IDepartmentRepository departmentRepository;
        IEmployeeRepository employeeRepository;

        public DepartmentController(IDepartmentRepository department, IEmployeeRepository employee)
        {
            departmentRepository = department;
            employeeRepository = employee;

        }
        public IActionResult Index(int ID=0)
        {
            List<Employee> employees = employeeRepository.GetAll();

            List<Department> departments = departmentRepository.GetAll();// context.departments.ToList();
            ViewData["id"]=ID;
            return View(departments);
        }
        public IActionResult New()
        {
            return View(new Department());

        }

        public IActionResult saveAdd(Department newdepartments)
        {
            if (ModelState.IsValid == true)

            {
                departmentRepository.Insert(newdepartments);
                //context.departments.Add(newdepartments);
                //context.SaveChanges();

                return RedirectToAction("Index", "Department");
            }


            return View("New", newdepartments);


        }


        //view for edit 
        public IActionResult Edit(int id)
        {
            Department departments = departmentRepository.GetById(id);// context.departments.FirstOrDefault(s => s.Id == id);
            return View(departments);

        }


        public IActionResult SaveEdit(int id, Department departments)
        {
            if (ModelState.IsValid == true)
            {
                departmentRepository.Update(id, departments);//Department olddepartments = context.departments.FirstOrDefault(s => s.Id == id);
                                                             //olddepartments.Id = departments.Id;
                                                             //olddepartments.Name = departments.Name;

                //context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View("Edit", departments);

        }

        //delete
        public IActionResult Delete(int id)
        {
            departmentRepository.Delete(id);
            //Department olddept = context.departments.FirstOrDefault(s => s.Id == id);
            //context.departments.Remove(olddept);
            //context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

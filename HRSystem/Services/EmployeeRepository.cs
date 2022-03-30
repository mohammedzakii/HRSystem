using HRSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HRSystem.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        HRSystemContext context;
        public EmployeeRepository(HRSystemContext _context)
        {
            context = _context;
        }
        public List<Employee> GetAll()
        {
            List<Employee> employees = context.employees.Include(d => d.Department).ToList();
            return (employees);
        }
        public Employee GetById(int id)
        {
            return context.employees.FirstOrDefault(i => i.Id == id);
        }
        public List<Employee> GetByName(string EmpName)
        {
            List<Employee> employees=  context.employees.Where(e => e.Name.Contains(EmpName)).ToList();
            return (employees);

        }
        public int Insert(Employee employee)
        {
            context.employees.Add(employee);
            int raws = context.SaveChanges();
            return raws;
        }
        public int Update(int id, Employee newEmp)
        {

            Employee oldEmp = context.employees.FirstOrDefault(s => s.Id == id);
            oldEmp.Name = newEmp.Name;
            oldEmp.Address = newEmp.Address;
            oldEmp.Email = newEmp.Email;
            oldEmp.Phone = newEmp.Phone;
            oldEmp.Gender = newEmp.Gender;
            oldEmp.DateOfBirth = newEmp.DateOfBirth;
            oldEmp.NationalID = newEmp.NationalID;
            oldEmp.Nationality = newEmp.Nationality;
            oldEmp.Salary = newEmp.Salary;
            oldEmp.DateOfContract = newEmp.DateOfContract;
            oldEmp.AttendanceTime = newEmp.AttendanceTime;
            oldEmp.LeavingTime = newEmp.LeavingTime;
            oldEmp.Notes = newEmp.Notes;
            oldEmp.DeptID = newEmp.DeptID;


            int raws = context.SaveChanges();
            return raws;
        }
        public int Delete(int id)
        {
            Employee Emp = context.employees.FirstOrDefault(i => i.Id == id);
            context.employees.Remove(Emp);
            int raws = context.SaveChanges();
            return raws;
        }
    }
}

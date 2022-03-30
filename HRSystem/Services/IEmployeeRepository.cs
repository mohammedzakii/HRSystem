using HRSystem.Models;
using System.Collections.Generic;

namespace HRSystem.Services
{
    public interface IEmployeeRepository
    {
        int Delete(int id);
        List<Employee> GetAll();
    
        Employee GetById(int id);
        int Insert(Employee employee);
        int Update(int id, Employee newEmp);
    }
}
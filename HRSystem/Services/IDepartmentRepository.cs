using HRSystem.Models;
using System.Collections.Generic;

namespace HRSystem.Services
{
    public interface IDepartmentRepository
    {
        int Delete(int id);
        List<Department> GetAll();
        Department GetById(int id);
        int Insert(Department newdepartments);
        int Update(int id, Department departments);
    }
}
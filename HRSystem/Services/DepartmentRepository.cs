using HRSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace HRSystem.Services
{
    public class DepartmentRepository : IDepartmentRepository
    {
        HRSystemContext context;
        public DepartmentRepository(HRSystemContext _context)
        {
            context = _context;
        }
        public List<Department> GetAll()
        {
            List<Department> departments = context.departments.ToList();

            return (departments);
        }
        public Department GetById(int id)
        {
            return context.departments.FirstOrDefault(s => s.Id == id);
        }
        public int Insert(Department newdepartments)
        {
            context.departments.Add(newdepartments);
            int raws = context.SaveChanges();
            return raws;
        }
        public int Update(int id, Department departments)
        {
            Department olddepartments = context.departments.FirstOrDefault(s => s.Id == id);
            olddepartments.Id = departments.Id;
            olddepartments.Name = departments.Name;

            int raws = context.SaveChanges();
            return raws;
        }
        public int Delete(int id)
        {
            Department olddept = context.departments.FirstOrDefault(s => s.Id == id);
            context.departments.Remove(olddept);
            int raws = context.SaveChanges();
            return raws;
        }
    }
}

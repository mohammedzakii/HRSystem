using HRSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HRSystem.Services
{
    public class AttentanceReportRepository : IAttentanceReportRepository
    {
        HRSystemContext context;
        public AttentanceReportRepository(HRSystemContext _context)
        {
            context = _context;
        }
        public List<AttendanceReport> GetAll()
        {
            List<AttendanceReport> reports = context.attendanceReports.Include(d => d.Employee.Department).ToList();
            return reports;
        }
        public AttendanceReport GetById(int id)
        {
            return context.attendanceReports.FirstOrDefault(a => a.Id == id);
        }
        public int Insert(AttendanceReport attendance)
        {
            context.attendanceReports.Add(attendance);
            int raws = context.SaveChanges();
            return raws;
        }
        public int Update(int id, AttendanceReport newattendance)
        {
            AttendanceReport oldatt = context.attendanceReports.FirstOrDefault(d => d.Id == id);
            oldatt.Date = newattendance.Date;
            oldatt.AttendanceTime = newattendance.AttendanceTime;
            oldatt.LeavingTime = newattendance.LeavingTime;
            int raws = context.SaveChanges();
            return raws;
        }
        public int Delete(int id)
        {

            AttendanceReport attendance = context.attendanceReports.FirstOrDefault(i => i.Id == id);
            context.attendanceReports.Remove(attendance);
            int raws = context.SaveChanges();
            return raws;
        }

    }

}

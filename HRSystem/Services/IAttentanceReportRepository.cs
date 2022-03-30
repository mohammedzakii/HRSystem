using HRSystem.Models;
using System.Collections.Generic;

namespace HRSystem.Services
{
    public interface IAttentanceReportRepository
    {
        int Delete(int id);
        List<AttendanceReport> GetAll();
        AttendanceReport GetById(int id);
        int Insert(AttendanceReport attendance);
        int Update(int id, AttendanceReport newattendance);
    }
}
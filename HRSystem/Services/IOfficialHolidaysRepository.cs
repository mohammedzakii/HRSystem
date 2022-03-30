using HRSystem.Models;
using System.Collections.Generic;

namespace HRSystem.Services
{
    public interface IOfficialHolidaysRepository
    {
        int Delete(int id);
        List<OfficialHolidays> GetAll();
        OfficialHolidays GetById(int id);
        int Insert(OfficialHolidays newoff);
        int Update(int id, OfficialHolidays offical);
    }
}
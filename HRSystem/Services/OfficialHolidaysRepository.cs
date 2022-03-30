using HRSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace HRSystem.Services
{
    public class OfficialHolidaysRepository : IOfficialHolidaysRepository
    {
        HRSystemContext context; //= new HRSystemContext();
        public OfficialHolidaysRepository(HRSystemContext _context)
        {
            context = _context;
        }
        public List<OfficialHolidays> GetAll()
        {
            List<OfficialHolidays> offHolday = context.officialHolidays.ToList();
            return offHolday;
        }
        public OfficialHolidays GetById(int id)
        {
            return context.officialHolidays.FirstOrDefault(s => s.Id == id);
        }
        public int Insert(OfficialHolidays newoff)
        {
            context.officialHolidays.Add(newoff);
            int raws = context.SaveChanges();
            return raws;
        }
        public int Update(int id, OfficialHolidays offical)
        {
            OfficialHolidays oldoff = context.officialHolidays.FirstOrDefault(s => s.Id == id);
            oldoff.Id = offical.Id;
            oldoff.Name = offical.Name;
            oldoff.Date = offical.Date;
            int raws = context.SaveChanges();
            return raws;
        }
        public int Delete(int id)
        {
            OfficialHolidays oldoff = context.officialHolidays.FirstOrDefault(s => s.Id == id);
            context.officialHolidays.Remove(oldoff);
            int raws = context.SaveChanges();
            return raws;
        }
    }
}

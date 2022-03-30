using HRSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace HRSystem.Services
{
    public class GeneralSettingRepository : IGeneralSettingRepository
    {
        HRSystemContext context; //= new HRSystemContext();
        public GeneralSettingRepository(HRSystemContext _context)
        {
            context = _context;    
        }
        public List<GeneralSetting> GetAll()
        {

            List<GeneralSetting> settingModel = context.generalSettings.ToList();
            return settingModel;
        }
        public GeneralSetting GetById(int id)
        {
            return context.generalSettings.FirstOrDefault(s => s.Id == id);
        }
        public GeneralSetting Get()
        {
            return context.generalSettings.FirstOrDefault();
        }
        public int Insert(GeneralSetting NewSetting)
        {
            context.generalSettings.Add(NewSetting);
            int raws = context.SaveChanges();
            return raws;
        }
        public int Update(int id, GeneralSetting newSetting)
        {
            GeneralSetting oldSetting = context.generalSettings.FirstOrDefault(s => s.Id == id);
            oldSetting.ExtraHours = newSetting.ExtraHours;
            oldSetting.DiscountHours = newSetting.DiscountHours;
            oldSetting.OffDAy1 = newSetting.OffDAy1;
            oldSetting.OffDAy2 = newSetting.OffDAy2;
            int raws = context.SaveChanges();
            return raws;
        }
        public int Delete(int id)
        {
            GeneralSetting setting = context.generalSettings.FirstOrDefault(d => d.Id == id);
            context.generalSettings.Remove(setting);
            int raws = context.SaveChanges();
            return raws;
        }
    }
}

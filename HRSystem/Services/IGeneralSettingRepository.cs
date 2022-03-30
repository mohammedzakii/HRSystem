using HRSystem.Models;
using System.Collections.Generic;

namespace HRSystem.Services
{
    public interface IGeneralSettingRepository
    {
        int Delete(int id);
        List<GeneralSetting> GetAll();
        GeneralSetting GetById(int id);
        GeneralSetting Get();
        int Insert(GeneralSetting NewSetting);
        int Update(int id, GeneralSetting newSetting);
    }
}
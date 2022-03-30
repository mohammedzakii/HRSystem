using HRSystem.Models;
using HRSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HRsystem.Controllers
{
    public class GeneralSettingController : Controller
    {
        IGeneralSettingRepository generalSettingServices;
        public GeneralSettingController(IGeneralSettingRepository gSRep)
        {
            generalSettingServices = gSRep;
        }
        public IActionResult Index(int ID=0)
        {
            List<GeneralSetting> settingModel = generalSettingServices.GetAll();
            ViewData["setting"] = settingModel;
            ViewData["id"] = ID;
            return View(new GeneralSetting());
        }
        public IActionResult SaveAdd(GeneralSetting NewSetting)
        {
            List<GeneralSetting> settingModel = generalSettingServices.GetAll();
            if (ModelState.IsValid == true && settingModel.Count==0)
            {
                generalSettingServices.Insert(NewSetting);  
                TempData["AlertMessage"] = "Setting Saved Successfuly";
                return RedirectToAction("Index", "GeneralSetting");
            }
            ViewData["id"] = 0;
            ViewData["setting"] = settingModel;
            return View("Index", NewSetting);
        }

        public IActionResult Edit(int id)
        {
            GeneralSetting settingModel = generalSettingServices.GetById(id);//db.generalSettings.FirstOrDefault(s => s.Id == id);

            return View(settingModel);
        }
        public IActionResult SaveEdit(int id, GeneralSetting newSetting)
        {


            if (ModelState.IsValid == true)

            {
                generalSettingServices.Update(id, newSetting);
                return RedirectToAction("Index");
            }
            return View("Edit", newSetting);
        }
        public IActionResult Delete(int id)
        {
            generalSettingServices.Delete(id);
            return RedirectToAction("Index");
        }


    }
}

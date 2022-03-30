using HRSystem.Models;
using HRSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace HRsystem.Controllers
{
    public class OfficialHolidaysController : Controller
    {
        IOfficialHolidaysRepository officialHolidaysRepository;
        public OfficialHolidaysController(IOfficialHolidaysRepository official)
        {
            officialHolidaysRepository = official;

        }
        public IActionResult Index()
        {
            List<OfficialHolidays> offHolday = officialHolidaysRepository.GetAll(); 
            ViewData["offHolday"] = offHolday;
            return View(new OfficialHolidays());
        }

        public IActionResult saveAdd(OfficialHolidays newoff)
        {
            if (ModelState.IsValid == true)
            {
                officialHolidaysRepository.Insert(newoff);
                return RedirectToAction("Index", "OfficialHolidays");
            }

            List<OfficialHolidays> offHolday = officialHolidaysRepository.GetAll();
            ViewData["offHolday"] = offHolday;
            return View("Index", newoff);
        }
        public IActionResult Edit(int id)
        {
            OfficialHolidays officialHolidays = officialHolidaysRepository.GetById(id);
            return View(officialHolidays);

        }

        public IActionResult SaveEdit(int id, OfficialHolidays offical)
        {
            if (ModelState.IsValid == true)
            {
                officialHolidaysRepository.Update(id, offical);
                return RedirectToAction("Index");

            }
            return View("Edit", offical);

        }
        public IActionResult Delete(int id)
        {
            officialHolidaysRepository.Delete(id);
            return RedirectToAction("Index");
        }









    }
}

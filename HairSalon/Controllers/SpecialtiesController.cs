using System.Collections.Generic;
using HairSalon.Models;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Controllers
{
    public class SpecialtiesController : Controller
    {
        [HttpGet("/specialties/new")]
        public ActionResult NewSpecialty()
        {
            List<SpecialtyClass> allSpecialties = SpecialtyClass.GetAll();
            return View("Specialty-New", allSpecialties);
        }

        [HttpPost("/specialties")]
        public ActionResult Create(string specialtyDescription)
        {
            SpecialtyClass newSpecialty = new SpecialtyClass(specialtyDescription);
            newSpecialty.Create();
            return RedirectToAction("Specialty-Index");
        }

        

    }
}

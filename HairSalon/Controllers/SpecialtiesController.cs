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

        [HttpGet("/specialties")]
        public ActionResult Index()
        {
            List<SpecialtyClass> allSpecialties = SpecialtyClass.GetAll();
            return View("Specialty-Index", allSpecialties);
        }

        [HttpPost("/specialties")]
        public ActionResult Create(string specialtyDescription)

        {
            SpecialtyClass newSpecialty = new SpecialtyClass(specialtyDescription);

            newSpecialty.Create();
            return RedirectToAction("Index");
        }

        [HttpPost("/specialties/{id}/delete")]
        public ActionResult Delete(int id)
        {
            SpecialtyClass selectedSpecialty = SpecialtyClass.Find(id);
            selectedSpecialty.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost("/specialties/delete")]
        public ActionResult DeleteAll()
        {
            SpecialtyClass.DeleteAll();
            return RedirectToAction("Index");
        }
    }
}
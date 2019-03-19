using System.Collections.Generic;
using HairSalon.Models;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Controllers
{
    public class StylistsController : Controller
    {
        [HttpPost("/stylists/{id}/clients/{clientId}/add")]
        public ActionResult AddClient(int id, int clientId)
        {
            StylistClass foundStylist = StylistClass.Find(id);
            foundStylist.AddClient(clientId);
            
            return RedirectToAction("ShowStylist");
        }

        [HttpPost("/stylists/{id}/specialties/{specialtyId}/add")]
        public ActionResult AddSpecialty(int id, int specialtyId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            StylistClass foundStylist = StylistClass.Find(id);
            foundStylist.AddSpecialty(specialtyId);

            return RedirectToAction("ShowStylist");
        }

        [HttpPost("/stylists")]
        public ActionResult Create(string stylistName)
        {
            StylistClass newStylist = new StylistClass(stylistName);
            newStylist.Save();
            return RedirectToAction("Index");
        }

        [HttpPost("/stylists/{id}/delete")]
        public ActionResult Delete(int id)
        {
            StylistClass selectedStylist = StylistClass.Find(id);
            selectedStylist.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost("/stylists/{id}/edit")]
        public ActionResult EditStylist(int id)
        {
            StylistClass selectedStylist = StylistClass.Find(id);
            return View("Stylist-Edit", selectedStylist);
        }

        [HttpPost("/stylists/{id}/update")]
        public ActionResult Update(int id, string stylistName)
        {
            StylistClass selectedStylist = StylistClass.Find(id);
            selectedStylist.SetName(stylistName);
            selectedStylist.Save();
            return RedirectToAction("Index");
        }

        [HttpPost("/stylists/delete")]
        public ActionResult DeleteAll()
        {
            StylistClass.DeleteAll();
            return RedirectToAction("Index");
        }

        [HttpPost("/stylists/{id}/clients/{clientId}/delete")]
        public ActionResult DeleteClient(int id, int clientId)
        {
            StylistClass foundStylist = StylistClass.Find(id);
            if (foundStylist != null)
            {
                foundStylist.DeleteClient(clientId);
            }

            return RedirectToAction("ShowStylist");
        }

        [HttpPost("/stylists/{id}/specialties/{specialtyId}/delete")]
        public ActionResult DeleteSpecialty(int id, int specialtyId)
        {
            StylistClass foundStylist = StylistClass.Find(id);
            if (foundStylist != null)
            {
                foundStylist.DeleteSpecialty(specialtyId);
            }

            return RedirectToAction("ShowStylist");
        }

        [HttpGet("/stylists")]
        public ActionResult Index()
        {
            List<StylistClass> allStylists = StylistClass.GetAll();
            return View("Stylist-Index", allStylists);
        }

        [HttpGet("/stylists/specialties/{specialtyId}")]
        public ActionResult GetStylists(int specialtyId)
        {
            List<StylistClass> stylists = StylistClass.GetStylistsWithSpecialty(specialtyId);
            return View("Stylist-Index", stylists);
        }

        [HttpGet("/stylists/{id}/clients/new")]
        public ActionResult NewClient(int id)
        {
            StylistClass foundStylist = StylistClass.Find(id);
            return View("Client-Add", foundStylist);
        }

        [HttpPost("/stylists/new")]
        public ActionResult NewStylist()
        {
            return View("Stylist-New");
        }

        [HttpGet("/stylists/{id}")]
        public ActionResult ShowStylist(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            StylistClass selectedStylist = StylistClass.Find(id);
            if (selectedStylist != null)
            {
                List<ClientClass> allClients = ClientClass.GetAll();
                List<SpecialtyClass> allSpecialties = SpecialtyClass.GetAll();
                model.Add("stylist", selectedStylist);
                model.Add("allClients", allClients);
                model.Add("allSpecialties", allSpecialties);
            }

            return View("Stylist-Client-Show", model);
        }

        [HttpGet("/stylists/{id}/clients/{clientId}")]
        public ActionResult ShowClients(int id, int clientId)
        {
            ClientClass client = ClientClass.Find(clientId);
            Dictionary<string, object> model = new Dictionary<string, object>();
            StylistClass stylist = StylistClass.Find(id);
            if (stylist != null)
            {
                model.Add("client", client);
                model.Add("stylist", stylist);
            }

            return View("Client-Index", model);
        }
    }
}
using System.Collections.Generic;
using HairSalon.Models;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Controllers
{
    public class StylistController : Controller
    {
        [HttpPost("/stylists/{stylistId}/clients")]
        public ActionResult AddClient(int stylistId, string clientName)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            StylistClass foundStylist = StylistClass.Find(stylistId);
            ClientClass newClient = new ClientClass(clientName, stylistId);
            newClient.Save();
            List<ClientClass> stylistClients = foundStylist.GetClients();
            model.Add("clients", stylistClients);
            model.Add("stylist", foundStylist);
            return View("Show", model);
        }

        [HttpPost("/stylists")]
        public ActionResult CreateStylist(string stylistName, string stylistType)
        {
            StylistClass newStylist = new StylistClass(stylistName, stylistType);
            newStylist.Save();
            return RedirectToAction("Index");
        }

        [HttpPost("/stylists/{id}/delete")]
        public ActionResult DeleteStylist(int id)
        {
            StylistClass selectedStylist = StylistClass.Find(id);
            selectedStylist.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet("/stylists")]
        public ActionResult Index()
        {
            List<StylistClass> allStylists = StylistClass.GetAll();
            return View(allStylists);
        }

        [HttpGet("/stylists/new")]
        public ActionResult NewStylist()
        {
            return View();
        }

        [HttpGet("/stylists/{id}")]
        public ActionResult ShowStylist(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            StylistClass selectedStylist = StylistClass.Find(id);
            if (selectedStylist != null)
            {
                List<ClientClass> stylistClient = selectedStylist.GetClients();
                model.Add("stylist", selectedStylist);
                model.Add("clients", stylistClient);
            }

            return View(model);
        }
    }
}
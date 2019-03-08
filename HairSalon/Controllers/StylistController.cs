using System.Collections.Generic;
using HairSalon.Models;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Controllers
{
    public class StylistsController : Controller
    {
        [HttpPost("/stylists/{stylistId}/clients/add")]
        public ActionResult AddClient(int stylistId, string clientName)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            StylistClass foundStylist = StylistClass.Find(stylistId);
            ClientClass newClient = new ClientClass(clientName, stylistId);
            newClient.Save();
            List<ClientClass> stylistClients = foundStylist.GetClients();
            model.Add("clients", stylistClients);
            model.Add("stylist", foundStylist);
            return View("Stylist-Show", model);
        }

        [HttpPost("/stylists")]
        public ActionResult Create(string stylistName, string stylistType)
        {
            StylistClass newStylist = new StylistClass(stylistName, stylistType);
            newStylist.Save();
            return RedirectToAction("Stylist-Index");
        }

        [HttpPost("/stylists/{id}/delete")]
        public ActionResult Delete(int id)
        {
            StylistClass selectedStylist = StylistClass.Find(id);
            selectedStylist.Delete(id);
            return RedirectToAction("Stylist-Delete");
        }

        [HttpPost("/stylists/{stylistId}/clients/{clientId}/delete")]
        public ActionResult DeleteClient(int stylistId, int clientId)
        {
            StylistClass foundStylist = StylistClass.Find(stylistId);
            if (foundStylist != null)
            {
                ClientClass client = ClientClass.Find(clientId);
                if (client != null)
                {
                    client.Delete(clientId);
                }
            }

            return View("Client-Delete", foundStylist);
        }

        [HttpGet("/stylists/{stylistId}/clients/{clientId}/edit")]
        public ActionResult EditClient(int stylistId, int clientId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            StylistClass stylist = StylistClass.Find(stylistId);
            model.Add("stylist", stylist);
            ClientClass client = ClientClass.Find(clientId);
            model.Add("client", client);
            return View("Client-Edit", model);
        }
        [HttpGet("/stylists")]
        public ActionResult Index()
        {
            List<StylistClass> allStylists = StylistClass.GetAll();
            return View("Stylist-Index", allStylists);
        }

        [HttpGet("/stylists/{stylistId}/clients/new")]
        public ActionResult NewClient(int stylistId)
        {
            StylistClass foundStylist = StylistClass.Find(stylistId);
            return View("Client-Add", foundStylist);
        }

        [HttpGet("/stylists/new")]
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
                List<ClientClass> stylistClients = selectedStylist.GetClients();
                model.Add("stylist", selectedStylist);
                model.Add("clients", stylistClients);
            }

            return View("Stylist-Show", model);
        }

        [HttpGet("/stylists/{stylistId}/clients/{clientId}")]
        public ActionResult ShowClients(int stylistId, int clientId)
        {
            ClientClass client = ClientClass.Find(clientId);
            Dictionary<string, object> model = new Dictionary<string, object>();
            StylistClass stylist = StylistClass.Find(stylistId);
            if (stylist != null)
            {
                model.Add("client", client);
                model.Add("stylist", stylist);
            }

            return View("Client-Index", model);
        }
    }
}

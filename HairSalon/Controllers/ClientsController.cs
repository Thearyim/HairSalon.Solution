using System.Collections.Generic;
using HairSalon.Models;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Controllers
{
    public class ClientsController : Controller
    {
        //[HttpGet("/specialties/new")]
        //public ActionResult NewSpecialty()
        //{
        //    List<SpecialtyClass> allSpecialties = SpecialtyClass.GetAll();
        //    return View("Specialty-New", allSpecialties);
        //}

        [HttpGet("/clients")]
        public ActionResult Index()
        {
            List<ClientClass> allClients = ClientClass.GetAll();
            List<StylistClass> allStylists = StylistClass.GetAll();
            Dictionary<string, object> model = new Dictionary<string, object>
            {
                ["clients"] = allClients,
                ["stylists"] = allStylists
            };

            return View("Client-Index", model);
        }

        [HttpPost("/clients")]
        public ActionResult Create(string clientName)
        {
            ClientClass newClient = new ClientClass(clientName);
            newClient.Save();
            return RedirectToAction("Index");
        }

        [HttpPost("/clients/{id}/delete")]
        public ActionResult Delete(int id)
        {
            ClientClass selectedClient = ClientClass.Find(id);
            selectedClient.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost("/clients/delete")]
        public ActionResult DeleteAll()
        {
            ClientClass.DeleteAll();
            return RedirectToAction("Index");
        }

        [HttpGet("/clients/{clientId}/edit")]
        public ActionResult EditClient(int clientId)
        {
            ClientClass selectedClient = ClientClass.Find(clientId);
            return View("Client-Edit", selectedClient);
        }

        [HttpPost("/clients/{clientId}/update")]
        public ActionResult Update(int clientId, string clientName)
        {
            ClientClass selectedClient = ClientClass.Find(clientId);
            selectedClient.SetName(clientName);
            selectedClient.Save();
            return RedirectToAction("Index");
        }
    }
}
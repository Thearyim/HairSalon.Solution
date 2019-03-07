using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;

namespace HairSalon.Controllers
{
    public class ClientsController : Controller
    {

      [HttpGet("/stylists/{stylistId}/clients/new")]
      public ActionResult NewClient(int stylistId)
      {
        StylistClass stylist = StylistClass.Find(stylistId);
        return View(stylist);
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

        return View(model);
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

        return View(foundStylist);
      }

        [HttpGet("/stylists/{stylistId}/clients/{clientId}/edit")]
        public ActionResult EditClient(int stylistId, int clientId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            StylistClass stylist = StylistClass.Find(stylistId);
            model.Add("stylist", stylist);
            ClientClass client = ClientClass.Find(clientId);
            model.Add("client", client);
            return View(model);
        }





        //[HttpPost("/clients/delete")]
        //public ActionResult DeleteAll()
        //{
        //  ClientClass.ClearAll();
        //  return View();
        //}

        //[HttpPost("/stylists/{stylistId}/clients/{clientId}")]
        //public ActionResult Update(int stylistId, int clientId, string newName)
        //{
        //  ClientClass client = ClientClass.Find(clientId);
        //  client..Edit(newname);
        //  Dictionary<string, object> model = new Dictionary<string, object>();
        //  StylistClass stylist = StylistClass.Find(stylistId);
        //  model.Add("stylist", stylist);
        //  model.Add("client", client);
        //  return View("Show", model);
        //}
    }
}

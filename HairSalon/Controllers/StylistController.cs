using System.Collections.Generic;
using HairSalon.Models;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Controllers
{
    public class StylistsController : Controller
    {

      [HttpGet("/stylists")]
      public ActionResult Index()
      {
        List<StylistClass> allStylists = StylistClass.GetAll();
        return View(allStylists);
      }

      [HttpGet("/stylists/new")]
      public ActionResult New()
      {
        return View();
      }

      [HttpPost("/stylists")]
      public ActionResult Create(string stylistName, string stylistType)
      {
        StylistClass newStylist = new StylistClass(stylistName, stylistType);
        newStylist.Save();
        return RedirectToAction("Index");
      }

      [HttpGet("/stylists/{id}")]
      public ActionResult Show(int id)
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        StylistClass selectedStylist = StylistClass.Find(id);
        if (selectedStylist != null)
        {
            List<ClientClass> stylistClients = selectedStylist.GetClients();
            model.Add("stylist", selectedStylist);
            model.Add("clients", stylistClients);
        }

        return View(model);
      }

      [HttpPost("/stylists/{id}/delete")]
      public ActionResult Delete(int id)
      {
          StylistClass selectedStylist = StylistClass.Find(id);
          selectedStylist.Delete(id);
          return RedirectToAction("Index");
      }

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








    }
}

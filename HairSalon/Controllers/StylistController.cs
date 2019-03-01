using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class StylistsController : Controller
  {

    [HttpGet("/stylists")]
    public ActionResult Index()
    {
      List<StylistClass> allStylists = Stylist.GetAll();
      return View(allStylists);
    }

    [HttpGet("/stylists/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/stylists")]
    public ActionResult Create(string stylistName)
    {
      StylistClass newStylist = new Stylist(stylistName);
      newStylist.Save();
      return RedirectToAction("Index");
    }

    [HttpGet("/stylists/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      StylistClass selectedStylist = Stylist.Find(id);
      List<Client> stylistClient = selectedStylist.GetClients();
      model.Add("stylist", selectedStylist);
      model.Add("clients", stylistClient);
      return View(model);
    }

    [HttpPost("/stylists/{id}/delete")]
    public ActionResult Delete(int id)
    {
      StylistClass selectedStylist = Stylist.Find(id);
      selectedStylist.Delete(id);
      return RedirectToAction("Index");
    }

    [HttpPost("/stylists/{stylistId}/clients")]
    public ActionResult Create(int stylistId, string clientName)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      StylistClass foundStylist = Stylist.Find(stylistId);
      Client newClient = new Client(stylistId, name);
      newClient.Save();
      List<Client> stylistClients = foundStylist.GetClients();
      model.Add("clients", stylistClients);
      model.Add("stylist", foundStylist);
      return View("Show", model);
    }

  }
}

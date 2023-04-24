using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class ClientsController : Controller
  {
    // get db context
    private readonly HairSalonContext _db;

    // constructor for controller
    public ClientsController(HairSalonContext db)
    {
      // copies db to private controller copy of db
      _db = db;
    }

    // routes
    public ActionResult Index()
    {
      List<Client> model = _db.Clients.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      // add viewbag to pass in select options
      // SelectList(data that will populate <option> elements, value, displayed text)
      ViewBag.StylistId = new SelectList(_db.Stylists, "StylistId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Client client)
    {
      // save to entity
      _db.Clients.Add(client);
      // save to db
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Client thisClient = _db.Clients
                             .FirstOrDefault(client => client.ClientId == id);
      _db.Clients.Remove(thisClient);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Client thisClient = _db.Clients
                             .Include(client => client.Stylist)
                             .FirstOrDefault(client => client.ClientId == id);
      return View(thisClient);
    }

    public ActionResult Edit(int id)
    {
      Client thisClient = _db.Clients
                             .Include(client => client.Stylist)
                             .FirstOrDefault(client => client.ClientId == id);
      ViewBag.StylistId = new SelectList(_db.Stylists, "StylistId", "Name");
      return View(thisClient);
    }

    [HttpPost]
    public ActionResult Edit(Client client)
    {
      _db.Clients.Update(client);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = client.ClientId});
    }

    public ActionResult Search()
    {
      return View();
    }

    [HttpPost]
    public ActionResult SearchResults(string searchTerm)
    {
      List<Client>  searchResults = _db.Clients
                                        .Where(client => client.FirstName.Contains(searchTerm) == true)
                                        .ToList();
      return View(searchResults);
    }

    public ActionResult AddAppointment(int id)
    {
      ViewBag.Client = _db.Clients
                          .Include(client => client.Stylist)
                          .FirstOrDefault(client => client.ClientId == id);
      return View();
    }

    // [HttpPost]
    // public ActionResult AddAppointment(Client client, string DateTime)
    // {
    //   // check to see if this client already has an appt scheduled for this time
    //   #nullable enable
    //   Appointment? thisAppointment = _db.Appointments.FirstOrDefault(appt => (appt.ClientId == client.ClientId && appt.DateTime == DateTime));
    //   #nullable disable
    //   if (thisAppointment == null && DateTime != null) {
    //     _db.Appointments.Add(new Appointment() {ClientId = client.ClientId, StylistId = client.StylistId, DateTime = DateTime});
    //     _db.SaveChanges();
    //   }
    //   // save to db
    //   return RedirectToAction("Index");
    // }
  }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class AppointmentsController : Controller
  {
    // get db context
    private readonly HairSalonContext _db;

    // constructor for controller
    public AppointmentsController(HairSalonContext db)
    {
      // copies db to private controller copy of db
      _db = db;
    }

    // routes
    public ActionResult Index()
    {
      List<Appointment> model = _db.Appointments
                                   .Include(appointment => appointment.Stylist)
                                   .Include(appointment => appointment.Client)
                                   .ToList();
      //  Get list of clients & pass to view 
      ViewBag.ClientId = new SelectList(_db.Clients, "ClientId", "FirstName");
      return View(model);
    }

    public ActionResult Create(int id)
    {
      // Client thisClient = _db.Clients
      //                        .FirstOrDefault(client => client.ClientId == id);
      ViewBag.ClientId = new SelectList(_db.Clients, "ClientId", "FirstName");
      return View();
    }

    [HttpPost]
    // public ActionResult Create(Client client, DateTime dateTime)
    public ActionResult Create(Appointment appointment)
    {
      // get info
      Client thisClient = _db.Clients.Include(c => c.Stylist)
                                      .FirstOrDefault(c => c.ClientId == appointment.ClientId);
      Appointment thisAppointment = new Appointment {
        ClientId = thisClient.ClientId,
        StylistId = thisClient.StylistId,
        DateTime = appointment.DateTime
      };
      // save to entity
      _db.Appointments.Add(thisAppointment);
      // save to db
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Appointment thisAppointment = _db.Appointments
                                       .Include(appointment => appointment.Stylist)
                                       .Include(appointment => appointment.Client)
                                       .FirstOrDefault(appointment => appointment.AppointmentId == id);
      return View(thisAppointment);
    }

    // public ActionResult Search()
    // {
    //   return View();
    // }

    // [HttpPost]
    // public ActionResult SearchResults(string searchTerm)
    // {
    //   List<Client>  searchResults = _db.Clients
    //                                     .Where(client => client.FirstName.Contains(searchTerm) == true)
    //                                     .ToList();
    //   return View(searchResults);
    // }
  }
}
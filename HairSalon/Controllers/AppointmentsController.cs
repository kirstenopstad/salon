using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
      ViewBag.ClientId = new SelectList(_db.Clients, "ClientId", "Name");
      return View(model);
    }

    // [HttpPost]
    // public ActionResult AddAppointment(int id) 
    // {
    //   // takes in client and redirects to Create appt
    //   return RedirectToAction("Create", new { id = id})

    // }

    // takes client id as route value
    public ActionResult Create(int id)
    {
      Client thisClient = _db.Clients
                             .Include(client => client.Stylist)
                             .FirstOrDefault(client => client.ClientId == id);
      return View(thisClient);
    }

    [HttpPost]
    public ActionResult Create(Appointment appointment)
    {
      // save to entity
      _db.Appointments.Add(appointment);
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
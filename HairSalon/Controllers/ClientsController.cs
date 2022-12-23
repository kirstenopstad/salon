using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
      ViewBag.StylistId = new SelectList(_db.Stylists, "StylistId", "Name") ;
      return View();
    }
  }
}
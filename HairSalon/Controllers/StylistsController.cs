using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class StylistsController : Controller
  {
    // get db context
    private readonly HairSalonContext _db;

    // constructor for controller
    public StylistsController(HairSalonContext db)
    {
      // copies db to private controller copy of db
      _db = db;
    }

    // routes
    public ActionResult Index()
    {
      List<Stylist> model = _db.Stylists.ToList();
      return View(model);
    }
  }
}
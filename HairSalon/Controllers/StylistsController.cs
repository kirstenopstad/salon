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

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Stylist stylist)
    {
      // add to entity
      _db.Stylists.Add(stylist);
      // save changes
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Stylist thisStylist = _db.Stylists
                               .Include(stylist => stylist.Clients)
                               .FirstOrDefault(stylist => stylist.StylistId == id);
      return View(thisStylist);
    }
    
    // Edit
    public ActionResult Edit(int id) 
    {
      Stylist thisStylist = _db.Stylists
                               .FirstOrDefault(stylist => stylist.StylistId == id);
      return View(thisStylist);
    }

    [HttpPost]
    public ActionResult Edit(Stylist stylist)
    {
      _db.Stylists.Update(stylist);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = stylist.StylistId} );
    }

    // Delete
    [HttpPost]
    public ActionResult Delete(int id)
    {
      Stylist thisStylist = _db.Stylists
                               .FirstOrDefault(stylist => stylist.StylistId == id);
      _db.Stylists.Remove(thisStylist);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Search()
    {
      return View();
    }

    [HttpPost]
    public ActionResult SearchResults(string searchTerm)
    {
      // Dictionary<string, object> model = new Dictionary<string, object>() {};
      List<Stylist>  searchResults = _db.Stylists
                                        .Where(stylist => stylist.Name.Contains(searchTerm) == true)
                                        .ToList();
      // model.Add(searchTerm, searchResults);
      return View(searchResults);
    }
  }
}
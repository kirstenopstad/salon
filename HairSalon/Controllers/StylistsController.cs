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

    // from tutorial: https://learn.microsoft.com/en-us/aspnet/core/tutorials/razor-pages/search?view=aspnetcore-6.0
    [BindProperty(SupportsGet = true)]
    public string ? SearchString { get; set; }

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

    [HttpGet]
    public ActionResult Search()
    {
      return View();
    }

    [HttpPost("?={SearchString}")]
    public ActionResult SearchResults()
    {
      List<Stylist>  searchResults = _db.Stylists
                                        .Where(stylist => stylist.Name.Contains(SearchString) == true)
                                        .ToList();
      return RedirectToAction("SearchResults", searchResults);
    }
  }
}